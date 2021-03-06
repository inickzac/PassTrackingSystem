using Microsoft.AspNetCore.Identity;
using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PassTrackingSystem.Infrastructure
{
    public class RandomDataGenerator
    {
        private ApplicationDBContext _dBContext;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private static string[] namesFemales = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.NamesFemale.txt");
        private static string[] lastNamesFemales = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.LastNamesFemale.txt");
        private static string[] patronymicFemales = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.PatronymicFemale.txt");
        private static string[] positions = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.Positions.txt");
        private static string[] placeOfWorks = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.Company.txt");
        private static string[] namesMales = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.NamesMale.txt");
        private static string[] lastNamesMales = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.LastNamesMale.txt");
        private static string[] patronymicMales = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.PatronymicMale.txt");
        private static string[] purposeOfIssuances = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.PurposeOfIssuance.txt");
        private static string[] cameras = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.Cameras.txt");
        private static string[] cars = getStringsFromFile(@"PassTrackingSystem.Resource.RandomGenerator.Cars.txt");
        public RandomDataGenerator(ApplicationDBContext dBContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dBContext = dBContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            AddInitDataToDBAsync();
        }

        private void CreateRole()
        {
            
            if(roleManager.Roles.Any())
            {
                _ = roleManager.CreateAsync(new IdentityRole { Name = "Administrator" }).Result;
                _ = roleManager.CreateAsync(new IdentityRole { Name = "Moderator" }).Result;
                _ = roleManager.CreateAsync(new IdentityRole { Name = "Operator" }).Result;
            }
        }
        
        private Car CreateCar()
        {
            return new Car
            {
                CarBrand = GetRandomStringFromCollections(cars),
                CarLicensePlate = $"{new Random().Next(1000, 9999)} {GenerateSeries()}-{new Random().Next(0, 9)}",
            };
        }

        private async void CreateRandomUsers()
        {
            if (!userManager.Users.Any())
            {
                var emloyee = _dBContext.Employees.ToArray();
                var role = roleManager.Roles.ToArray();
                for (int i = 0; i < 100; i++)
                {
                    var user = new AppUser
                    {
                        EmployeeId = GetRandomObjectFromCollections(emloyee).Id,
                        UserName = Guid.NewGuid().ToString()
                    };                  
                    _= userManager.CreateAsync(user, "123").Result;
                    _= userManager.AddToRoleAsync(user, GetRandomObjectFromCollections(role).Name).Result;
                } 
            }
        }

        
        private void CreateCarPasses(int quatity = 3000)
        {
            if (!_dBContext.CarPasses.Any())
            {
                var tpes = _dBContext.Visitors.ToArray();
                var employees = _dBContext.Employees.ToArray();
                for (int i = 0; i < quatity; i++)
                {
                    _dBContext.CarPasses.Add(
                           new CarPass
                           {
                               ValidWith = DateTime.Now.AddMonths(new Random().Next(-120, -1)),
                               ValitUntil = DateTime.Now.AddMonths(new Random().Next(-119, 120)),
                               PurposeOfIssuance = GetRandomStringFromCollections(purposeOfIssuances),
                               CarPassIssued = GetRandomObjectFromCollections(employees),
                               VisitorId = GetRandomObjectFromCollections(tpes).Id,
                               Car = CreateCar()
                           }); ;
                }
                _dBContext.SaveChanges();
            }
        }
        private void CreateShootingPermission(int quatity = 3000)
        {
            if (!_dBContext.ShootingPermissions.Any())
            {
                var tpes = _dBContext.Visitors.ToArray();
                var fasilitys = _dBContext.StationFacilities.ToArray();
                var employees = _dBContext.Employees.ToArray();
                for (int i = 0; i < quatity; i++)
                {
                    _dBContext.ShootingPermissions.Add(
                           new ShootingPermission
                           {
                               ValidWith = DateTime.Now.AddMonths(new Random().Next(-120, -1)),
                               ValitUntil = DateTime.Now.AddMonths(new Random().Next(-119, 120)),
                               ShootingPurpose = GetRandomStringFromCollections(purposeOfIssuances),
                               StationFacilities = GetRandomObjectsFromCollections(fasilitys),
                               ShootingPermissionIssued = GetRandomObjectFromCollections(employees),
                               VisitorId = GetRandomObjectFromCollections(tpes).Id,
                               CameraType = GetRandomStringFromCollections(cameras),
                           }); ;
                }
                _dBContext.SaveChanges();
            }
        }
        private void CreateSinglePass(int quatity = 3000)
        {
            if (!_dBContext.SinglePasses.Any())
            {
                var tpes = _dBContext.Visitors.ToArray();
                var fasilitys = _dBContext.StationFacilities.ToArray();
                var employees = _dBContext.Employees.ToArray();
                for (int i = 0; i < quatity; i++)
                {
                    _dBContext.SinglePasses.Add(
                           new SinglePass
                           {
                               ValidWith = DateTime.Now.AddMonths(new Random().Next(-120, -1)),
                               ValitUntil = DateTime.Now.AddMonths(new Random().Next(-119, 120)),
                               PurposeOfIssuance = GetRandomStringFromCollections(purposeOfIssuances),
                               StationFacilities = GetRandomObjectsFromCollections(fasilitys),
                               SinglePassIssued = GetRandomObjectFromCollections(employees),
                               VisitorId = GetRandomObjectFromCollections(tpes).Id
                           }); ;
                }
                _dBContext.SaveChanges();
            }
        }
        private void CreateTemporaryPass(int quatity = 3000)
        {
            if (!_dBContext.TemporaryPasses.Any())
            {
                var tpes = _dBContext.Visitors.ToArray();
                var fasilitys = _dBContext.StationFacilities.ToArray();
                var employees = _dBContext.Employees.ToArray();
                for (int i = 0; i < quatity; i++)
                {
                    _dBContext.TemporaryPasses.Add(
                           new TemporaryPass
                           {
                               ValidWith = DateTime.Now.AddMonths(new Random().Next(-120, -1)),
                               ValitUntil = DateTime.Now.AddMonths(new Random().Next(-119, 120)),
                               PurposeOfIssuance = GetRandomStringFromCollections(purposeOfIssuances),
                               StationFacilities = GetRandomObjectsFromCollections(fasilitys),
                               TemporaryPassIssued = GetRandomObjectFromCollections(employees),
                               VisitorId = GetRandomObjectFromCollections(tpes).Id
                           }); ;
                }
                _dBContext.SaveChanges();
            }
        }
        private void CreateStationFacility()
        {
            if (!_dBContext.StationFacilities.Any())
            {
                _dBContext.StationFacilities.AddRange(new StationFacility { Value = "Насосная1" },
                    new StationFacility { Value = "Электрощитовая" },
                    new StationFacility { Value = "Насосная2" },
                    new StationFacility { Value = "Насосная3" },
                    new StationFacility { Value = "Насосная4" },
                    new StationFacility { Value = "Насосная5" },
                    new StationFacility { Value = "Насосная6" });
                _dBContext.SaveChanges();
            }
        }
        private void CreateDepartments()
        {
            if (!_dBContext.Departments.Any())
            {
                _dBContext.Departments.AddRange(new Department { Value = "Отдел продаж" },
              new Department { Value = "Технический отдел" },
              new Department { Value = "Руководство" },
              new Department { Value = "Бухгалтерия" },
              new Department { Value = "Охрана" },
              new Department { Value = "Отдел закупак" },
              new Department { Value = "Отдел продаж" });
                _dBContext.SaveChanges();
            }
        }

        public void CreateEmployees()
        {
            if (!_dBContext.Employees.Any())
            {
                for (int i = 0; i < 1000; i++)
                {
                    Employee employee = new Employee
                    {
                        Position = GetRandomStringFromCollections(positions),
                        Department = GetRandomObjectFromCollections(_dBContext.Departments.ToArray())
                    };
                    var Random = new Random().Next(1, 2);
                    if (Random == 1)
                    {
                        employee.Name = GetRandomStringFromCollections(namesFemales);
                        employee.LastName = GetRandomStringFromCollections(lastNamesFemales);
                        employee.Patronymic = GetRandomStringFromCollections(patronymicFemales);
                    }
                    if (Random == 2)
                    {
                        employee.Name = GetRandomStringFromCollections(namesMales);
                        employee.LastName = GetRandomStringFromCollections(lastNamesMales);
                        employee.Patronymic = GetRandomStringFromCollections(patronymicMales);
                    }
                    _dBContext.Employees.Add(employee);
                }
                _dBContext.SaveChanges();
            }
        }
        private void CreateDocumentTypes()
        {
            if (!_dBContext.DocumentTypes.Any())
            {
                var values = new List<string> { "Паспорт",
               "Водительское удостовириение",
               "Удостоверение личности"};
                for (int i = 0; i < 100; i++)
                {
                    values.Add(Guid.NewGuid().ToString());
                }

                values.ForEach(val => _dBContext.DocumentTypes.Add(new DocumentType { Value = val }));
                _dBContext.SaveChanges();
            }
        }

        private void CreateIssuingAuthority()
        {
            if (!_dBContext.IssuingAuthorities.Any())
            {
                var values = new List<string> { "КГБ",
               "РОВД",
               "МВД",
            "ГАИ"};
                values.ForEach(val => _dBContext.IssuingAuthorities.Add(new IssuingAuthority { Value = val }));
                _dBContext.SaveChanges();
            }
        }
        private void CreateVisitors(int quantity)
        {
            if (!_dBContext.Visitors.Any())
            {
                for (int i = 0; i < quantity; i++)
                {
                    Visitor visitor = new Visitor
                    {
                        Document = GenerateRandomDocument(),
                        PlaceOfWork = GetRandomStringFromCollections(placeOfWorks),
                        Position = GetRandomStringFromCollections(positions)
                    };
                    var Random = new Random().Next(1, 2);
                    if (Random == 1)
                    {
                        visitor.Name = GetRandomStringFromCollections(namesFemales);
                        visitor.LastName = GetRandomStringFromCollections(lastNamesFemales);
                        visitor.Patronymic = GetRandomStringFromCollections(patronymicFemales);
                    }
                    if (Random == 2)
                    {
                        visitor.Name = GetRandomStringFromCollections(namesMales);
                        visitor.LastName = GetRandomStringFromCollections(lastNamesMales);
                        visitor.Patronymic = GetRandomStringFromCollections(patronymicMales);
                    }
                    _dBContext.Visitors.Add(visitor);
                }
                _dBContext.SaveChanges();
            }

        }
        public Document GenerateRandomDocument()
        {
            return new Document
            {
                Series = GenerateSeries(),
                Number = new Random().Next(0, 999999).ToString(),
                DateOfIssue = DateTime.Now.AddMonths(new Random().Next(-120, -1)),
                DocumentType = _dBContext.DocumentTypes.Skip(new Random()
             .Next(_dBContext.DocumentTypes
             .Count() - 1)).First(),
                IssuingAuthority = _dBContext.IssuingAuthorities.Skip(new Random()
             .Next(_dBContext.IssuingAuthorities
             .Count() - 1)).First()
            };
        }
        private string GenerateSeries()
        {
            int startP = 65;
            int endPos = 90;
            var alphabet = new List<char>();
            for (int i = startP; i <= endPos; i++)
            {
                alphabet.Add((char)i);
            }
            return alphabet[new Random().Next(alphabet.Count)].ToString()
                + alphabet[new Random().Next(alphabet.Count)].ToString();
        }
        private static string[] getStringsFromFile(string Path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(Path))
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd().Split("\r\n");
                if (result.Count() > 0)
                {
                    return result;
                }
                throw new InvalidOperationException("failed to process file");
            }

        }
        private static string GetRandomStringFromCollections(string[] values)
        {
            if (values?.Length > 0)
            {
                return values[new Random().Next(values.Length - 1)];
            }

            throw new ArgumentNullException("collection must not be empty");
        }

        private static T GetRandomObjectFromCollections<T>(T[] values)
        {
            if (values?.Length > 0)
            {
                return values[new Random().Next(1,9999999) % values.Length];
            }
            throw new ArgumentNullException("collection must not be empty");
        }

        private static List<T> GetRandomObjectsFromCollections<T>(T[] values) =>
            values.Select(x => new { val = x, rand = new Random().NextDouble() >= 0.5 })
                .Where(x => x.rand).Select(x => x.val).ToList();

        private void AddInitDataToDBAsync()
        {
            CreateDepartments();
            CreateEmployees();
            CreateRole();
            //CreateRandomUsers();
            CreateIssuingAuthority();
            CreateDocumentTypes();
            CreateVisitors(1000);
            CreateStationFacility();
            CreateSinglePass();
            CreateTemporaryPass();
            CreateShootingPermission();
            CreateCarPasses();
        }
    }




}
