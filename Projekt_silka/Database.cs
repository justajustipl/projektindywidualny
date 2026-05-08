using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Collections.Generic;

namespace Projekt_silka
{
    public class Database
    {
        private readonly string _connectionString;

        public Database()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ProjektSilka");
            Directory.CreateDirectory(folder);
            string dbPath = Path.Combine(folder, "data.db");
            _connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Clients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Login TEXT UNIQUE NOT NULL,
                    Password TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Employees (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Login TEXT UNIQUE NOT NULL,
                    Password TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Trainings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    EmployeeId INTEGER NOT NULL,
                    Date TEXT NOT NULL,
                    Time TEXT NOT NULL,
                    MaxSlots INTEGER NOT NULL DEFAULT 10,
                    Room TEXT DEFAULT '',
                    FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                );
                CREATE TABLE IF NOT EXISTS Registrations (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ClientId INTEGER NOT NULL,
                    TrainingId INTEGER NOT NULL,
                    FOREIGN KEY(ClientId) REFERENCES Clients(Id),
                    FOREIGN KEY(TrainingId) REFERENCES Trainings(Id)
                );
                CREATE TABLE IF NOT EXISTS Availability (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmployeeId INTEGER NOT NULL,
                    Date TEXT NOT NULL,
                    TimeStart TEXT NOT NULL,
                    TimeEnd TEXT NOT NULL,
                    FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                );";
            cmd.ExecuteNonQuery();

            // Safe migration — adds Room column if it doesn't exist yet
            try
            {
                var alter = conn.CreateCommand();
                alter.CommandText = "ALTER TABLE Trainings ADD COLUMN Room TEXT DEFAULT ''";
                alter.ExecuteNonQuery();
            }
            catch { /* column already exists, ignore */ }

            SeedDemoData(conn);
        }

        private void SeedDemoData(SqliteConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Employees";
            long count = (long)cmd.ExecuteScalar();
            if (count > 0) return;

            cmd.CommandText = "INSERT INTO Employees (Name, Login, Password) VALUES ('Anna Kowalska', 'anna', '1234')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Employees (Name, Login, Password) VALUES ('Jan Nowak', 'jan', '1234')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Clients (Name, Login, Password) VALUES ('Piotr Wiśniewski', 'piotr', '1234')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Clients (Name, Login, Password) VALUES ('Marta Zielińska', 'marta', '1234')";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Trainings (Title, EmployeeId, Date, Time, MaxSlots, Room) VALUES ('Indywidualny', 1, '2026-06-01', '10:00', 1, '')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Trainings (Title, EmployeeId, Date, Time, MaxSlots, Room) VALUES ('Grupowy', 1, '2026-06-02', '12:00', 15, 'Sala 1')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Trainings (Title, EmployeeId, Date, Time, MaxSlots, Room) VALUES ('W parze', 2, '2026-06-03', '09:00', 2, '')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Trainings (Title, EmployeeId, Date, Time, MaxSlots, Room) VALUES ('Grupowy', 2, '2026-06-04', '14:00', 15, 'Sala 2')";
            cmd.ExecuteNonQuery();
        }

        // --- AUTH ---

        public (int id, string name)? LoginClient(string login, string password)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Clients WHERE Login=@l AND Password=@p";
            cmd.Parameters.AddWithValue("@l", login);
            cmd.Parameters.AddWithValue("@p", password);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return (reader.GetInt32(0), reader.GetString(1));
            return null;
        }

        public (int id, string name)? LoginEmployee(string login, string password)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Employees WHERE Login=@l AND Password=@p";
            cmd.Parameters.AddWithValue("@l", login);
            cmd.Parameters.AddWithValue("@p", password);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return (reader.GetInt32(0), reader.GetString(1));
            return null;
        }

        // --- TRAININGS ---

        public List<Training> GetAllTrainings()
        {
            var list = new List<Training>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT t.Id, t.Title, e.Name, t.Date, t.Time, t.MaxSlots,
                       (SELECT COUNT(*) FROM Registrations r WHERE r.TrainingId = t.Id) AS Registered,
                       t.Room
                FROM Trainings t
                JOIN Employees e ON e.Id = t.EmployeeId
                ORDER BY t.Date, t.Time";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(new Training
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    EmployeeName = reader.GetString(2),
                    Date = reader.GetString(3),
                    Time = reader.GetString(4),
                    MaxSlots = reader.GetInt32(5),
                    RegisteredCount = reader.GetInt32(6),
                    Room = reader.IsDBNull(7) ? "" : reader.GetString(7)
                });
            return list;
        }

        public void AddTraining(string title, int employeeId, string date, string time, int maxSlots, string room = "")
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Trainings (Title, EmployeeId, Date, Time, MaxSlots, Room)
                                VALUES (@title, @eid, @date, @time, @max, @room)";
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@eid", employeeId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@max", maxSlots);
            cmd.Parameters.AddWithValue("@room", room);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTraining(int trainingId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd1 = conn.CreateCommand();
            cmd1.CommandText = "DELETE FROM Registrations WHERE TrainingId=@id";
            cmd1.Parameters.AddWithValue("@id", trainingId);
            cmd1.ExecuteNonQuery();

            var cmd2 = conn.CreateCommand();
            cmd2.CommandText = "DELETE FROM Trainings WHERE Id=@id";
            cmd2.Parameters.AddWithValue("@id", trainingId);
            cmd2.ExecuteNonQuery();
        }

        public bool IsRoomTaken(string room, string date, string time, int excludeTrainingId = -1)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*) FROM Trainings
                                WHERE Room=@room AND Date=@date AND Time=@time AND Id != @exclude";
            cmd.Parameters.AddWithValue("@room", room);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@exclude", excludeTrainingId);
            return (long)cmd.ExecuteScalar() > 0;
        }

        // --- REGISTRATIONS ---

        public bool RegisterClient(int clientId, int trainingId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var check = conn.CreateCommand();
            check.CommandText = "SELECT COUNT(*) FROM Registrations WHERE ClientId=@c AND TrainingId=@t";
            check.Parameters.AddWithValue("@c", clientId);
            check.Parameters.AddWithValue("@t", trainingId);
            if ((long)check.ExecuteScalar() > 0) return false;

            var slotCheck = conn.CreateCommand();
            slotCheck.CommandText = @"SELECT t.MaxSlots,
                                        (SELECT COUNT(*) FROM Registrations r WHERE r.TrainingId=t.Id)
                                      FROM Trainings t WHERE t.Id=@t";
            slotCheck.Parameters.AddWithValue("@t", trainingId);
            using var reader = slotCheck.ExecuteReader();
            if (reader.Read())
            {
                int maxSlots = reader.GetInt32(0);
                int registered = reader.GetInt32(1);
                if (registered >= maxSlots) return false;
            }

            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Registrations (ClientId, TrainingId) VALUES (@c, @t)";
            cmd.Parameters.AddWithValue("@c", clientId);
            cmd.Parameters.AddWithValue("@t", trainingId);
            cmd.ExecuteNonQuery();
            return true;
        }

        public void CancelRegistration(int clientId, int trainingId)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Registrations WHERE ClientId=@c AND TrainingId=@t";
            cmd.Parameters.AddWithValue("@c", clientId);
            cmd.Parameters.AddWithValue("@t", trainingId);
            cmd.ExecuteNonQuery();
        }

        public List<Training> GetClientRegistrations(int clientId)
        {
            var list = new List<Training>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT t.Id, t.Title, e.Name, t.Date, t.Time, t.MaxSlots,
                       (SELECT COUNT(*) FROM Registrations r WHERE r.TrainingId = t.Id),
                       t.Room
                FROM Trainings t
                JOIN Employees e ON e.Id = t.EmployeeId
                JOIN Registrations reg ON reg.TrainingId = t.Id
                WHERE reg.ClientId = @c
                ORDER BY t.Date, t.Time";
            cmd.Parameters.AddWithValue("@c", clientId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(new Training
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    EmployeeName = reader.GetString(2),
                    Date = reader.GetString(3),
                    Time = reader.GetString(4),
                    MaxSlots = reader.GetInt32(5),
                    RegisteredCount = reader.GetInt32(6),
                    Room = reader.IsDBNull(7) ? "" : reader.GetString(7)
                });
            return list;
        }

        public void RescheduleRegistration(int clientId, int oldTrainingId, int newTrainingId)
        {
            CancelRegistration(clientId, oldTrainingId);
            RegisterClient(clientId, newTrainingId);
        }

        // --- AVAILABILITY ---

        public void AddAvailability(int employeeId, string date, string timeStart, string timeEnd)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Availability (EmployeeId, Date, TimeStart, TimeEnd)
                                VALUES (@eid, @date, @ts, @te)";
            cmd.Parameters.AddWithValue("@eid", employeeId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@ts", timeStart);
            cmd.Parameters.AddWithValue("@te", timeEnd);
            cmd.ExecuteNonQuery();
        }

        public List<AvailabilitySlot> GetEmployeeAvailability(int employeeId)
        {
            var list = new List<AvailabilitySlot>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT Id, Date, TimeStart, TimeEnd FROM Availability
                                WHERE EmployeeId=@eid ORDER BY Date, TimeStart";
            cmd.Parameters.AddWithValue("@eid", employeeId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(new AvailabilitySlot
                {
                    Id = reader.GetInt32(0),
                    Date = reader.GetString(1),
                    TimeStart = reader.GetString(2),
                    TimeEnd = reader.GetString(3)
                });
            return list;
        }

        public List<Training> GetEmployeeTrainings(int employeeId)
        {
            var list = new List<Training>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT t.Id, t.Title, e.Name, t.Date, t.Time, t.MaxSlots,
                       (SELECT COUNT(*) FROM Registrations r WHERE r.TrainingId = t.Id),
                       t.Room
                FROM Trainings t
                JOIN Employees e ON e.Id = t.EmployeeId
                WHERE t.EmployeeId = @eid
                ORDER BY t.Date, t.Time";
            cmd.Parameters.AddWithValue("@eid", employeeId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(new Training
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    EmployeeName = reader.GetString(2),
                    Date = reader.GetString(3),
                    Time = reader.GetString(4),
                    MaxSlots = reader.GetInt32(5),
                    RegisteredCount = reader.GetInt32(6),
                    Room = reader.IsDBNull(7) ? "" : reader.GetString(7)
                });
            return list;
        }
    }

    // --- MODEL CLASSES ---

    public class Training
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string Date { get; set; } = "";
        public string Time { get; set; } = "";
        public int MaxSlots { get; set; }
        public int RegisteredCount { get; set; }
        public string Room { get; set; } = "";
        public override string ToString()
        {
            string roomInfo = Room != "" ? $" | {Room}" : "";
            return $"{Date} {Time} | {Title} | Trener: {EmployeeName} | {RegisteredCount}/{MaxSlots} miejsc{roomInfo}";
        }
    }

    public class AvailabilitySlot
    {
        public int Id { get; set; }
        public string Date { get; set; } = "";
        public string TimeStart { get; set; } = "";
        public string TimeEnd { get; set; } = "";
        public override string ToString() => $"{Date} | {TimeStart} - {TimeEnd}";
    }
}