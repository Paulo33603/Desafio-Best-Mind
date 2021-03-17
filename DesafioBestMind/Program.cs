using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DesafioBestMind
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao;

            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1- Login");
            Console.WriteLine("2- Cadastrar\n");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Create();
                    break;
                default:
                    Console.WriteLine("Opção inexistente");
                    Main(null);
                    break;
            }
        }

        public static void Login()
        {
            string email, password, sql;

            do
            {
                Console.WriteLine("\nEmail:");
                email = Console.ReadLine();
                Console.WriteLine("\nSenha:");
                password = Console.ReadLine();
                
                sql = $"SELECT `email`, `password` FROM `user` WHERE `email` = '{email}' AND `password` = MD5('{password}')";
                
                DataBaseConnection().Open();

                var cmd = new MySqlCommand(sql, DataBaseConnection());
                var da = new MySqlDataAdapter(cmd);
                var dtMensagens = new DataTable();

                da.Fill(dtMensagens);

                if (dtMensagens.Rows.Count > 0)
                {
                    dtMensagens.Rows[0]["email"].ToString();
                    dtMensagens.Rows[0]["password"].ToString();
                    Home(email);
                }

                Console.WriteLine("Email ou senha inválidos");

                DataBaseConnection().Close();
            } while (email == String.Empty || password == String.Empty);
        }

        public static void Home(string email)
        {
            Console.WriteLine("\nSeja Bem-Vindo");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\nAções:");
            Console.WriteLine("1- Sair | 2- Trocar de conta | 3- Criar uma nova conta");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Environment.Exit(0);
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    Create();
                    break;
                default:
                    Console.WriteLine("Opção inexistente");
                    Home(email);
                    break;
            }
        }

        public static void Create()
        {
            string name, cpf, email, cellPhone, password, passwordConfirmation;

            Console.WriteLine("\nDigite seu nome completo");
            name = Console.ReadLine();

            do
            {
                Console.WriteLine("\nDigite seu cpf");
                cpf = Console.ReadLine();

                if (!CpfValidation(cpf))
                {
                    Console.WriteLine("\nCPF inválido");
                    cpf = String.Empty;
                }

                if (ExistCpf(cpf))
                {
                    Console.WriteLine("\nCPF já cadastrado");
                    cpf = String.Empty;
                }
            } while (cpf == String.Empty);

            do
            {
                Console.WriteLine("\nDigite seu email");
                email = Console.ReadLine();

                if (ExistEmail(email))
                {
                    Console.WriteLine("Email já cadastrado");
                    email = String.Empty;
                }
            } while (email == String.Empty);

            Console.WriteLine("\nDigite seu número de celular");
            cellPhone = Console.ReadLine();

            do
            {
                Console.WriteLine("\nCrie uma senha");
                password = Console.ReadLine();
                Console.WriteLine("\nConfirme sua senha");
                passwordConfirmation = Console.ReadLine();

                if (passwordConfirmation != password)
                {
                    Console.Write("\nAs senhas não são iguais");
                    password = String.Empty;
                    passwordConfirmation = String.Empty;
                }
            } while (password == String.Empty);

            string sql = $"INSERT INTO `user`(`name`, `cpf`, `email`, `cellPhone`, `password`) VALUE('{name}', '{cpf}', '{email}', '{cellPhone}', MD5('{password}'))";

            ExecuteInsert(sql);

            Home(email);
        }

        public static void ExecuteInsert(string sql)
        {
            DataBaseConnection().Open();

            var cmd = new MySqlCommand(sql, DataBaseConnection());
            var da = new MySqlDataAdapter(cmd);

            var dtMensagens = new DataTable();
            da.Fill(dtMensagens);

            DataBaseConnection().Close();
        }

        public static bool ExistEmail(string email)
        {
            DataBaseConnection().Open();

            string sql = $"SELECT `email` FROM `user` WHERE email = '{email}'";
            var cmd = new MySqlCommand(sql, DataBaseConnection());
            var da = new MySqlDataAdapter(cmd);
            var dtMensagens = new DataTable();

            da.Fill(dtMensagens);

            DataBaseConnection().Close();

            if (dtMensagens.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static bool ExistCpf(string cpf)
        {
            DataBaseConnection().Open();

            string sql = $"SELECT `cpf` FROM `user` WHERE cpf = '{cpf}'";
            var cmd = new MySqlCommand(sql, DataBaseConnection());
            var da = new MySqlDataAdapter(cmd);
            var dtMensagens = new DataTable();

            da.Fill(dtMensagens);

            DataBaseConnection().Close();

            if (dtMensagens.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static bool CpfValidation(string cpf)
        {
            cpf = cpf.Replace("-", "").Replace(".", "").Replace(" ", "");

            if (cpf.Length == 11)
            {
                if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" ||
                    cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" ||
                    cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" ||
                    cpf == "99999999999")
                {
                    return false;
                }
                else
                {
                    int num1 = int.Parse(cpf.Substring(0, 1));
                    int num2 = int.Parse(cpf.Substring(1, 1));
                    int num3 = int.Parse(cpf.Substring(2, 1));
                    int num4 = int.Parse(cpf.Substring(3, 1));
                    int num5 = int.Parse(cpf.Substring(4, 1));
                    int num6 = int.Parse(cpf.Substring(5, 1));
                    int num7 = int.Parse(cpf.Substring(6, 1));
                    int num8 = int.Parse(cpf.Substring(7, 1));
                    int num9 = int.Parse(cpf.Substring(8, 1));
                    int num10 = int.Parse(cpf.Substring(9, 1));
                    int num11 = int.Parse(cpf.Substring(10, 1));

                    int total = num1 * 10 + num2 * 9 + num3 * 8 +
                                num4 * 7 + num5 * 6 + num6 * 5 +
                                num7 * 4 + num8 * 3 + num9 * 2;

                    int divisao = total / 11;

                    int resto = total - 11 * divisao;

                    int verificador1;

                    if (resto < 2)
                    {
                        verificador1 = 0;
                    }
                    else
                    {
                        verificador1 = 11 - resto;
                    }

                    total = num1 * 11 + num2 * 10 + num3 * 9 + num4 * 8 +
                            num5 * 7 + num6 * 6 + num7 * 5 +
                            num8 * 4 + num9 * 3 + num10 * 2;

                    divisao = total / 11;

                    resto = total - (11 * divisao);

                    int verificador2;

                    if (resto < 2)
                    {
                        verificador2 = 0;
                    }
                    else
                    {
                        verificador2 = 11 - resto;
                    }

                    if (verificador1 == num10 && verificador2 == num11)
                    {
                        return true;
                    }

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static MySqlConnection DataBaseConnection()
        {
            string connectionString = "Server=localhost;Database=db_best_mind;Uid=root;Pwd=1234;";
            var cnn = new MySqlConnection(connectionString);

            return cnn;
        }
    }
}
