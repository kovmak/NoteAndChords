namespace Note_and_Chords
{
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    class Program
    {
        static string PATH_TO_DATA = @"Data\";
        static string PATH_TO_NOTES = @"notes.txt";
        static string PATH_TO_CHORDS = @"chords.txt";
        static string PATH_TO_USE = @"use.txt";
        static string PATH_TO_APP = @"app.txt";

        static char separatorSymbol = '\t';
        static List<User> users = new List<User>();
        static User currentUser;

        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Default;
            LoadUsers();
            MainMenu();
        }

        static void LoadUsers()
        {
            string pathToUsers = "Data\\users.txt";
            if (File.Exists(pathToUsers))
            {
                string[] lines = File.ReadAllLines(pathToUsers);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        users.Add(new User { Username = parts[0], Password = parts[1] });
                    }
                }
            }
        }

        static void SaveUsers()
        {
            string pathToUsers = "Data\\users.txt";
            List<string> lines = users.Select(u => $"{u.Username},{u.Password}").ToList();
            File.WriteAllLines(pathToUsers, lines);
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("----------------------------------------Welcome to the Notes and Chords program---------------------------------------\n");
            Console.ResetColor();
            if (currentUser == null)
            {
                Console.WriteLine(" 1. Login");
                Console.WriteLine(" 2. Register");
                Console.WriteLine(" 3. About the application\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [ESC] Log out");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($" Welcome, {currentUser.Username}!\n");
                Console.ResetColor();

                if (IsAdmin())
                {
                    Console.WriteLine(" View pages:");
                    Console.WriteLine(" [1] View Notes page");
                    Console.WriteLine(" [2] View Chords page");
                    Console.WriteLine(" [3] View Usage and Purpose page\n");
                    Console.WriteLine("Other options:");
                    Console.WriteLine(" [4] Search");
                    Console.WriteLine(" [5] Listen to notes");
                    Console.WriteLine(" [6] Edit data \n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" [ESC] Log out");
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine(" View pages:");
                    Console.WriteLine(" [1] View Notes page");
                    Console.WriteLine(" [2] View Chords page");
                    Console.WriteLine(" [3] View Usage and Purpose page\n");
                    Console.WriteLine("Other options:");
                    Console.WriteLine(" [4] Search");
                    Console.WriteLine(" [5] Listen to notes \n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" [ESC] Log out");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    if (currentUser == null)
                    {
                        Login();
                    }
                    else
                    {
                        ViewData(PATH_TO_DATA + PATH_TO_NOTES, "--------------------------------------------Notes:--------------------------------------------");
                    }
                    break;
                case ConsoleKey.D2:
                    if (currentUser == null)
                    {
                        Register();
                    }
                    else
                    {
                        ViewData(PATH_TO_DATA + PATH_TO_CHORDS, "--------------------------------------------Chords:--------------------------------------------");
                    }
                    break;
                case ConsoleKey.D3:
                    if (currentUser == null)
                    {
                        ViewData(PATH_TO_DATA + PATH_TO_APP, "--------------------------------------------Information about the app:--------------------------------------------");
                    }
                    else
                    {
                        ViewData(PATH_TO_DATA + PATH_TO_USE, "--------------------------------------------Usage and purpose:--------------------------------------------");
                    }
                    break;
                case ConsoleKey.D4:
                    if (currentUser != null)
                    {
                        SearchAllFiles();
                    }
                    break;
                case ConsoleKey.D5:
                    {
                        Player();
                    }
                    break;
                case ConsoleKey.D6:
                    if (currentUser != null && IsAdmin())
                    {
                        Edit();
                    }
                    break;
                case ConsoleKey.Escape:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n_ Thank you for using the app, see you soon!");
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n An incorrect option is selected");
                    Console.ResetColor();
                    Console.ReadKey();
                    MainMenu();
                    break;
            }

            if (currentUser != null)
            {
                MainMenu();
            }
        }

        static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-------------------------------------------Authorization page-------------------------------------------\n");
            Console.ResetColor();
            Console.Write(" Enter a username: ");
            string username = Console.ReadLine();
            Console.Write(" Enter your password: ");
            string password = MaskedPasswordInput();
            User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                currentUser = user;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Login successful! Press Enter to continue.");
                Console.ResetColor();
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n The username or password is incorrect. Press Enter to try again.");
                Console.ResetColor();
                Console.ReadKey();
                MainMenu();
            }
        }

        static string MaskedPasswordInput()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (char.IsControl(key.KeyChar))
                    continue;
                password += key.KeyChar;
                Console.Write("*");
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        static void Register()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-------------------------------------------Registration page-------------------------------------------\n");
            Console.ResetColor();
            Console.Write(" Enter a new username: ");
            string username = Console.ReadLine();
            if (users.Any(u => u.Username == username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n A user with this name already exists. Press Enter to try again.");
                Console.ResetColor();
                Console.ReadKey();
                MainMenu();
                return;
            }
            string password;
            string confirmPassword;
            do
            {
                Console.Write(" Enter a new password: ");
                password = MaskedPasswordInput();
                Console.Write(" Confirm your password: ");
                confirmPassword = MaskedPasswordInput();
                if (password != confirmPassword)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Passwords do not match. Please try again.\n");
                    Console.ResetColor();
                }
            } while (password != confirmPassword);
            users.Add(new User { Username = username, Password = password });
            SaveUsers();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Registration is successful! Press Enter to continue.");
            Console.ResetColor();
            Console.ReadKey();
            MainMenu();
        }

        static bool IsAdmin()
        {
            return currentUser != null && currentUser.Username.ToLower() == "admin";
        }

        static void SearchAllFiles()
        {
            List<string> allPaths = new List<string>
            {
              PATH_TO_DATA + PATH_TO_NOTES,
              PATH_TO_DATA + PATH_TO_CHORDS,
              PATH_TO_DATA + PATH_TO_USE
            };

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("--------------------------------------------Search--------------------------------------------\n");
            Console.ResetColor();
            Console.WriteLine(" Enter a keyword");
            string keyWord = Console.ReadLine().ToLower();
            bool searchIsSuccess = false;
            foreach (string path in allPaths)
            {
                string dataStringFromFile = InputData(path);
                string[] arrayOfStringData = dataStringFromFile.Split('\n');

                List<string> arrayOfSearchRecord = new List<string>();
                foreach (string str in arrayOfStringData)
                {
                    if (str.ToLower().IndexOf(keyWord) != -1)
                    {
                        searchIsSuccess = true;
                        arrayOfSearchRecord.Add(str);
                    }
                }
                if (searchIsSuccess)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n Search results for a file: {path}\n");
                    PrintData(arrayOfSearchRecord.ToArray());
                    Console.ResetColor();
                }
            }
            if (!searchIsSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n The keyword was not found in any of the files. Make sure it is spelled correctly and try again.");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n Press any key");
            Console.ReadKey();
        }

        static void PrintData(String[] outputRecords)
        {
            foreach (String stringData in outputRecords)
            {
                foreach (var separateField in stringData.Split(separatorSymbol))
                {
                    Console.WriteLine(separateField);
                }
            }
        }

        static void ViewData(string path, string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            String dataStringFromFile = InputData(path);
            PrintData(dataStringFromFile.Split('\n'));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n Press any key");
            Console.ReadKey();
            Console.ResetColor();
            MainMenu();
        }

        static string InputData(string path)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string errorMasage = "File not found";
            Console.ResetColor();
            if (File.Exists(path) && (File.ReadAllText(path) != ""))
            {
                return File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine(errorMasage);
                return "";
            }
        }

        static void Edit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(@"--------------------------------------------Data editing:--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" [1] Storinku Noti");
            Console.WriteLine(" [2] Acordi bar");
            Console.WriteLine(" [3] The Usage and Purpose bar");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ESC Log out");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n Press the 1-3 key");
            int Edit = (int)Console.ReadKey().KeyChar;
            switch (Edit)
            {
                case 49:
                    EditData(PATH_TO_DATA + PATH_TO_NOTES, "--------------------------------------------Notes:--------------------------------------------");
                    break;
                case 50:
                    EditData(PATH_TO_DATA + PATH_TO_CHORDS, "--------------------------------------------Chords:--------------------------------------------");
                    break;
                case 51:
                    EditData(PATH_TO_DATA + PATH_TO_USE, "--------------------------------------------Usage and purpose:--------------------------------------------");
                    break;
                case 27:
                    return;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Wrong key selected or no file found");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
            }
        }

        static void PlayTone(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }

        static void Player()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("--------------------------------------------Listen to the notes:--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;

            var notesFrequency = new Dictionary<string, int>()
            {
                {"C", 261},
                {"D", 293},
                {"E", 329},
                {"F", 349},
                {"G", 392},
                {"A", 440},
                {"B", 493}
            };

            Console.WriteLine("\n Enter the notes you want to play (for example, CDEFGAB):");
            string input = Console.ReadLine().ToUpper();

            Console.WriteLine("\n Enter the note duration (in milliseconds, for example, 500):");
            int duration = Convert.ToInt32(Console.ReadLine());

            foreach (char note in input)
            {
                if (notesFrequency.ContainsKey(note.ToString()))
                {
                    PlayTone(notesFrequency[note.ToString()], duration);
                    Thread.Sleep(duration);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" Note '{note}' is not found in the dictionary.");
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n Press any key to return to the menu");
            Console.ReadKey();
            Console.ResetColor();
            MainMenu();
        }

        static void EditData(string path, string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            NormalizeData(path);
            String[] arrayOfStringData = InputData(path).Split('\n');
            PrintData(arrayOfStringData);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nSelect the data to edit");
            Console.ResetColor();
            int numberOfEditRecord = new int();
            String numberOfEditRecordString = Console.ReadLine();
            try
            {
                numberOfEditRecord = Int32.Parse(numberOfEditRecordString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("You edit " + numberOfEditRecord + " record");
            Console.WriteLine(arrayOfStringData[numberOfEditRecord]);
            String newData = "";
            foreach (String str in arrayOfStringData[numberOfEditRecord].Split(separatorSymbol))
            {
                Console.WriteLine("You want to change: ");
                Console.WriteLine("(+/ -)");
                if (Console.ReadKey().KeyChar == '+')
                {
                    newData += Console.ReadLine();
                }
                else
                {
                    newData += str;
                }
            }
            Console.WriteLine("Would you like to add a new field? (+/-)");
            while (Console.ReadKey().KeyChar == '+')
            {
                newData += '\n' + Console.ReadLine();
            }
            arrayOfStringData[numberOfEditRecord] = newData;
            File.WriteAllLines(path, arrayOfStringData);
        }

        static void NormalizeData(String path)
        {
            String dataStringFromFile = InputData(path);
            dataStringFromFile = dataStringFromFile.Replace("\r", "");
            while (dataStringFromFile.IndexOf("\n\n") != -1)
            {
                dataStringFromFile = dataStringFromFile.Replace("\n\n", "\n");
            }
            if (dataStringFromFile.Substring(dataStringFromFile.Length - 1) == "\n")
            {
                dataStringFromFile = dataStringFromFile.Remove(dataStringFromFile.Length - 1);
            }
            File.WriteAllText(path, dataStringFromFile);
        }
    }
}