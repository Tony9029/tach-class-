using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static List<Account> accounts = new List<Account>();
    static List<Project> projects = new List<Project>();
    static List<Employee> employees = new List<Employee>();
    static List<Meeting> meetings = new List<Meeting>();

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        InitializeData();

        Account currentUser = Login();

        if (currentUser != null)
        {
            if (currentUser.IsAdmin)
            {
                while (true)
                {
                    AdminMenu();
                }
            }
            else
            {
                while (true)
                {
                    UserMenu();
                }
            }
        }
        else
        {
            Console.WriteLine("Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
        }
    }

    static void InitializeData()
    {
        accounts.Add(new Account("admin", "admin", true));
        accounts.Add(new Account("user1", "user1", false));
        accounts.Add(new Account("user2", "user2", false));

        Project project1 = new Project("P001", "Project 1", "Target 1", DateTime.Now, DateTime.Now.AddDays(30), "Description 1", "Đang tiến hành");
        projects.Add(project1);

        employees.Add(new Employee("NV001", "John Doe", "john@example.com", true, 30));
        employees.Add(new Employee("NV002", "Jane Smith", "jane@example.com", false, 25));
    }

    static Account Login()
    {
        Console.Write("Tên đăng nhập: ");
        string username = Console.ReadLine();
        Console.Write("Mật khẩu: ");
        string password = Console.ReadLine();

        foreach (Account account in accounts)
        {
            if (account.Username == username && account.Password == password)
            {
                return account;
            }
        }

        return null;
    }

    static void AdminMenu()
    {
        Console.WriteLine("=== Menu quản trị ===");
        Console.WriteLine("1. Thêm dự án");
        Console.WriteLine("2. Sửa dự án");
        Console.WriteLine("3. Xóa dự án");
        Console.WriteLine("4. Thêm công việc");
        Console.WriteLine("5. Xóa công việc");
        Console.WriteLine("6. Cập nhật tiến độ công việc");
        Console.WriteLine("7. Thêm thành viên vào nhóm");
        Console.WriteLine("8. Xóa thành viên khỏi nhóm");
        Console.WriteLine("9. Thống kê số lượng nhân viên");
        Console.WriteLine("10. Thêm comment");
        Console.WriteLine("11. Thêm, sửa, xóa cuộc họp");
        Console.WriteLine("12. Tìm kiếm dự án");
        Console.WriteLine("13. Xem danh sách công việc trong dự án");
        Console.WriteLine("14. Thêm nhân viên");
        Console.WriteLine("15. Sửa thông tin nhân viên");
        Console.WriteLine("16. Xóa nhân viên");
        Console.WriteLine("17. Thoát");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    AddProject();
                    break;
                case 2:
                    EditProject();
                    break;
                case 3:
                    DeleteProject();
                    break;
                case 4:
                    AddTask();
                    break;
                case 5:
                    RemoveTask();
                    break;
                case 6:
                    UpdateTaskProgress();
                    break;
                case 7:
                    AddMemberToTeam();
                    break;
                case 8:
                    RemoveMemberFromTeam();
                    break;
                case 9:
                    CountEmployees();
                    break;
                case 10:
                    AddComment();
                    break;
                case 11:
                    ManageMeeting();
                    break;
                case 12:
                    SearchProject();
                    break;
                case 13:
                    ViewTasksInProject();
                    break;
                case 14:
                    AddEmployee();
                    break;
                case 15:
                    EditEmployee();
                    break;
                case 16:
                    DeleteEmployee();
                    break;
                case 17:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }

    static void UserMenu()
    {
        Console.WriteLine("=== Menu người dùng ===");
        Console.WriteLine("1. Cập nhật tiến độ công việc");
        Console.WriteLine("2. Xem tiến độ dự án");
        Console.WriteLine("3. Thêm comment");
        Console.WriteLine("4. Thêm, sửa, xóa cuộc họp");
        Console.WriteLine("5. Tìm kiếm dự án");
        Console.WriteLine("6. Thoát");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    UpdateTaskProgress();
                    break;
                case 2:
                    ViewProjectProgress();
                    break;
                case 3:
                    AddComment();
                    break;
                case 4:
                    ManageMeeting();
                    break;
                case 5:
                    SearchProject();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }

    static void AddProject()
    {
        Console.WriteLine("=== Thêm dự án ===");
        Console.Write("ID dự án: ");
        string id = Console.ReadLine();
        Console.Write("Tên dự án: ");
        string name = Console.ReadLine();
        Console.Write("Mục tiêu: ");
        string target = Console.ReadLine();
        DateTime startDate;
        do
        {
            Console.Write("Ngày bắt đầu (dd/MM/yyyy): ");
            string startDateStr = Console.ReadLine();
            if (DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ngày bắt đầu không hợp lệ.");
            }
        } while (true);

        DateTime endDate;
        do
        {
            Console.Write("Ngày kết thúc (dd/MM/yyyy): ");
            string endDateStr = Console.ReadLine();
            if (DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                if (endDate >= startDate)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ngày kết thúc phải sau ngày bắt đầu.");
                }
            }
            else
            {
                Console.WriteLine("Ngày kết thúc không hợp lệ.");
            }
        } while (true);

        Console.Write("Mô tả: ");
        string description = Console.ReadLine();
        Console.Write("Trạng thái (chưa tiến hành/đang tiến hành/đã hoàn thành): ");
        string status = Console.ReadLine();

        Project newProject = new Project(id, name, target, startDate, endDate, description, status);
        projects.Add(newProject);
        Console.WriteLine("Dự án đã được thêm thành công.");
    }

    static void EditProject()
    {
        Console.WriteLine("=== Sửa dự án ===");
        Console.Write("Nhập ID của dự án cần sửa: ");
        string id = Console.ReadLine();
        Project projectToEdit = projects.FirstOrDefault(p => p.ID == id);
        if (projectToEdit != null)
        {
            Console.WriteLine("Nhập thông tin mới cho dự án (nhấn Enter để giữ nguyên):");

            Console.Write($"Tên dự án ({projectToEdit.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
                projectToEdit.Name = name;

            Console.Write($"Mục tiêu ({projectToEdit.Target}): ");
            string target = Console.ReadLine();
            if (!string.IsNullOrEmpty(target))
                projectToEdit.Target = target;

            DateTime startDate;
            do
            {
                Console.Write($"Ngày bắt đầu ({projectToEdit.StartDate:dd/MM/yyyy}): ");
                string startDateStr = Console.ReadLine();
                if (string.IsNullOrEmpty(startDateStr))
                {
                    break;
                }
                else if (DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
                {
                    projectToEdit.StartDate = startDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Ngày bắt đầu không hợp lệ.");
                }
            } while (true);

            DateTime endDate;
            do
            {
                Console.Write($"Ngày kết thúc ({projectToEdit.EndDate:dd/MM/yyyy}): ");
                string endDateStr = Console.ReadLine();
                if (string.IsNullOrEmpty(endDateStr))
                {
                    break;
                }
                else if (DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
                {
                    if (endDate >= projectToEdit.StartDate)
                    {
                        projectToEdit.EndDate = endDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ngày kết thúc phải sau ngày bắt đầu.");
                    }
                }
                else
                {
                    Console.WriteLine("Ngày kết thúc không hợp lệ.");
                }
            } while (true);

            Console.Write($"Mô tả ({projectToEdit.Description}): ");
            string description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description))
                projectToEdit.Description = description;

            Console.Write($"Trạng thái ({projectToEdit.Status}): ");
            string status = Console.ReadLine();
            if (!string.IsNullOrEmpty(status))
                projectToEdit.Status = status;

            Console.WriteLine("Dự án đã được cập nhật thành công.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án cần sửa.");
        }
    }

    static void DeleteProject()
    {
        Console.WriteLine("=== Xóa dự án ===");
        Console.Write("Nhập ID của dự án cần xóa: ");
        string id = Console.ReadLine();
        Project projectToRemove = projects.FirstOrDefault(p => p.ID == id);
        if (projectToRemove != null)
        {
            projects.Remove(projectToRemove);
            Console.WriteLine("Dự án đã được xóa thành công.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án cần xóa.");
        }
    }

    static void AddTask()
    {
        Console.WriteLine("=== Thêm công việc ===");
        Console.Write("ID công việc: ");
        string id = Console.ReadLine();
        Console.Write("Tên công việc: ");
        string name = Console.ReadLine();
        DateTime startDate;
        do
        {
            Console.Write("Ngày bắt đầu (dd/MM/yyyy): ");
            string startDateStr = Console.ReadLine();
            if (DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ngày bắt đầu không hợp lệ.");
            }
        } while (true);

        DateTime endDate;
        do
        {
            Console.Write("Ngày kết thúc (dd/MM/yyyy): ");
            string endDateStr = Console.ReadLine();
            if (DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                if (endDate >= startDate)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ngày kết thúc phải sau ngày bắt đầu.");
                }
            }
            else
            {
                Console.WriteLine("Ngày kết thúc không hợp lệ.");
            }
        } while (true);

        Console.Write("Tiến độ (%): ");
        int progress;
        while (!int.TryParse(Console.ReadLine(), out progress) || progress < 0 || progress > 100)
        {
            Console.WriteLine("Tiến độ phải là một số nguyên từ 0 đến 100.");
            Console.Write("Tiến độ (%): ");
        }

        Task newTask = new Task(id, name, startDate, endDate, progress);
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            selectedProject.Tasks.Add(newTask);
            Console.WriteLine("Công việc đã được thêm thành công vào dự án " + selectedProject.Name);
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void RemoveTask()
    {
        Console.WriteLine("=== Xóa công việc ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.Write("Nhập ID công việc cần xóa: ");
            string taskId = Console.ReadLine();
            Task taskToRemove = selectedProject.Tasks.FirstOrDefault(t => t.ID == taskId);
            if (taskToRemove != null)
            {
                selectedProject.Tasks.Remove(taskToRemove);
                Console.WriteLine("Công việc đã được xóa khỏi dự án " + selectedProject.Name);
            }
            else
            {
                Console.WriteLine("Không tìm thấy công việc cần xóa trong dự án " + selectedProject.Name);
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void UpdateTaskProgress()
    {
        Console.WriteLine("=== Cập nhật tiến độ công việc ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.Write("Nhập ID công việc cần cập nhật: ");
            string taskId = Console.ReadLine();
            Task taskToUpdate = selectedProject.Tasks.FirstOrDefault(t => t.ID == taskId);
            if (taskToUpdate != null)
            {
                Console.Write("Tiến độ mới (%): ");
                int newProgress;
                while (!int.TryParse(Console.ReadLine(), out newProgress) || newProgress < 0 || newProgress > 100)
                {
                    Console.WriteLine("Tiến độ phải là một số nguyên từ 0 đến 100.");
                    Console.Write("Tiến độ (%): ");
                }
                taskToUpdate.Progress = newProgress;
                Console.WriteLine("Tiến độ công việc đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy công việc cần cập nhật trong dự án " + selectedProject.Name);
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void AddMemberToTeam()
    {
        Console.WriteLine("=== Thêm thành viên vào nhóm ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.Write("Nhập tên thành viên: ");
            string memberName = Console.ReadLine();
            selectedProject.Members.Add(memberName);
            Console.WriteLine("Thành viên đã được thêm vào nhóm của dự án " + selectedProject.Name);
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void RemoveMemberFromTeam()
    {
        Console.WriteLine("=== Xóa thành viên khỏi nhóm ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.Write("Nhập tên thành viên cần xóa: ");
            string memberName = Console.ReadLine();
            if (selectedProject.Members.Contains(memberName))
            {
                selectedProject.Members.Remove(memberName);
                Console.WriteLine("Thành viên đã được xóa khỏi nhóm của dự án " + selectedProject.Name);
            }
            else
            {
                Console.WriteLine("Không tìm thấy thành viên trong nhóm của dự án " + selectedProject.Name);
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void CountEmployees()
    {
        Console.WriteLine("=== Thống kê số lượng nhân viên ===");
        Console.WriteLine("Tổng số nhân viên: " + employees.Count);
    }

    static void AddComment()
    {
        Console.WriteLine("=== Thêm comment ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.Write("Nhập comment: ");
            string comment = Console.ReadLine();
            selectedProject.Comments.Add(comment);
            Console.WriteLine("Comment đã được thêm vào dự án " + selectedProject.Name);
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void ManageMeeting()
    {
        Console.WriteLine("=== Quản lý cuộc họp ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.WriteLine("1. Thêm cuộc họp");
            Console.WriteLine("2. Sửa cuộc họp");
            Console.WriteLine("3. Xóa cuộc họp");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddMeeting(selectedProject);
                        break;
                    case 2:
                        EditMeeting(selectedProject);
                        break;
                    case 3:
                        DeleteMeeting(selectedProject);
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void AddMeeting(Project project)
    {
        Console.WriteLine("=== Thêm cuộc họp ===");
        Console.Write("Ngày cuộc họp (dd/MM/yyyy): ");
        DateTime meetingDate;
        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out meetingDate))
        {
            project.Meetings.Add(meetingDate);
            Console.WriteLine("Cuộc họp đã được thêm vào dự án " + project.Name);
        }
        else
        {
            Console.WriteLine("Ngày cuộc họp không hợp lệ.");
        }
    }

    static void EditMeeting(Project project)
    {
        Console.WriteLine("=== Sửa cuộc họp ===");
        Console.Write("Nhập ngày của cuộc họp cần sửa (dd/MM/yyyy): ");
        DateTime meetingDate;
        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out meetingDate))
        {
            if (project.Meetings.Contains(meetingDate))
            {
                Console.Write("Ngày mới của cuộc họp (dd/MM/yyyy): ");
                DateTime newMeetingDate;
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out newMeetingDate))
                {
                    project.Meetings.Remove(meetingDate);
                    project.Meetings.Add(newMeetingDate);
                    Console.WriteLine("Ngày của cuộc họp đã được cập nhật.");
                }
                else
                {
                    Console.WriteLine("Ngày mới của cuộc họp không hợp lệ.");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy cuộc họp vào ngày này.");
            }
        }
        else
        {
            Console.WriteLine("Ngày của cuộc họp không hợp lệ.");
        }
    }

    static void DeleteMeeting(Project project)
    {
        Console.WriteLine("=== Xóa cuộc họp ===");
        Console.Write("Nhập ngày của cuộc họp cần xóa (dd/MM/yyyy): ");
        DateTime meetingDate;
        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out meetingDate))
        {
            if (project.Meetings.Contains(meetingDate))
            {
                project.Meetings.Remove(meetingDate);
                Console.WriteLine("Cuộc họp vào ngày " + meetingDate.ToString("dd/MM/yyyy") + " đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy cuộc họp vào ngày này.");
            }
        }
        else
        {
            Console.WriteLine("Ngày của cuộc họp không hợp lệ.");
        }
    }

    static void SearchProject()
    {
        Console.WriteLine("=== Tìm kiếm dự án ===");
        Console.Write("Nhập ID hoặc tên dự án: ");
        string keyword = Console.ReadLine();
        List<Project> matchedProjects = projects.Where(p => p.ID.Contains(keyword) || p.Name.Contains(keyword)).ToList();
        if (matchedProjects.Count > 0)
        {
            Console.WriteLine("Kết quả:");
            foreach (Project project in matchedProjects)
            {
                Console.WriteLine(project);
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án nào phù hợp.");
        }
    }

    static void ViewTasksInProject()
    {
        Console.WriteLine("=== Xem danh sách công việc trong dự án ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            if (selectedProject.Tasks.Count > 0)
            {
                Console.WriteLine("Danh sách công việc trong dự án " + selectedProject.Name + ":");
                foreach (Task task in selectedProject.Tasks)
                {
                    Console.WriteLine(task);
                }
            }
            else
            {
                Console.WriteLine("Dự án không có công việc nào.");
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }

    static void AddEmployee()
    {
        Console.WriteLine("=== Thêm nhân viên ===");
        Console.Write("Mã nhân viên: ");
        string id = Console.ReadLine();
        Console.Write("Tên nhân viên: ");
        string name = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Là quản trị viên (true/false): ");
        bool isAdmin;
        while (!bool.TryParse(Console.ReadLine(), out isAdmin))
        {
            Console.WriteLine("Vui lòng nhập true hoặc false.");
            Console.Write("Là quản trị viên (true/false): ");
        }

        Console.Write("Tuổi: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
        {
            Console.WriteLine("Vui lòng nhập một số nguyên dương.");
            Console.Write("Tuổi: ");
        }

        Employee newEmployee = new Employee(id, name, email, isAdmin, age);
        employees.Add(newEmployee);
        Console.WriteLine("Nhân viên đã được thêm vào danh sách.");
    }

    static void EditEmployee()
    {
        Console.WriteLine("=== Sửa thông tin nhân viên ===");
        Console.Write("Nhập mã nhân viên cần sửa: ");
        string id = Console.ReadLine();
        Employee employeeToEdit = employees.FirstOrDefault(e => e.ID == id);
        if (employeeToEdit != null)
        {
            Console.WriteLine("Nhập thông tin mới cho nhân viên (nhấn Enter để giữ nguyên):");

            Console.Write($"Tên nhân viên ({employeeToEdit.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
                employeeToEdit.Name = name;

            Console.Write($"Email ({employeeToEdit.Email}): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
                employeeToEdit.Email = email;

            Console.Write($"Là quản trị viên ({employeeToEdit.IsAdmin}): ");
            string isAdminStr = Console.ReadLine();
            bool isAdmin;
            if (!string.IsNullOrEmpty(isAdminStr) && bool.TryParse(isAdminStr, out isAdmin))
                employeeToEdit.IsAdmin = isAdmin;

            Console.Write($"Tuổi ({employeeToEdit.Age}): ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
            {
                Console.WriteLine("Vui lòng nhập một số nguyên dương.");
                Console.Write("Tuổi: ");
            }
            employeeToEdit.Age = age;

            Console.WriteLine("Thông tin nhân viên đã được cập nhật.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy nhân viên cần sửa.");
        }
    }

    static void DeleteEmployee()
    {
        Console.WriteLine("=== Xóa nhân viên ===");
        Console.Write("Nhập mã nhân viên cần xóa: ");
        string id = Console.ReadLine();
        Employee employeeToRemove = employees.FirstOrDefault(e => e.ID == id);
        if (employeeToRemove != null)
        {
            employees.Remove(employeeToRemove);
            Console.WriteLine("Nhân viên đã được xóa khỏi danh sách.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy nhân viên cần xóa.");
        }
    }

    static Project SelectProject()
    {
        Console.Write("Nhập ID của dự án: ");
        string projectId = Console.ReadLine();
        Project selectedProject = projects.FirstOrDefault(p => p.ID == projectId);
        return selectedProject;
    }

    static void ViewProjectProgress()
    {
        Console.WriteLine("=== Xem tiến độ dự án ===");
        Project selectedProject = SelectProject();
        if (selectedProject != null)
        {
            Console.WriteLine("Tiến độ dự án " + selectedProject.Name + ": " + selectedProject.GetProgress() + "%");
        }
        else
        {
            Console.WriteLine("Không tìm thấy dự án.");
        }
    }
}

class Account
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public Account(string username, string password, bool isAdmin)
    {
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
    }
}

class Project
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Target { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public List<Task> Tasks { get; set; }
    public List<string> Members { get; set; }
    public List<string> Comments { get; set; }
    public List<DateTime> Meetings { get; set; }

    public Project(string id, string name, string target, DateTime startDate, DateTime endDate, string description, string status)
    {
        ID = id;
        Name = name;
        Target = target;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        Status = status;
        Tasks = new List<Task>();
        Members = new List<string>();
        Comments = new List<string>();
        Meetings = new List<DateTime>();
    }

    public int GetProgress()
    {
        if (Tasks.Count == 0)
        {
            return 0;
        }
        else
        {
            int totalProgress = 0;
            foreach (Task task in Tasks)
            {
                totalProgress += task.Progress;
            }
            return totalProgress / Tasks.Count;
        }
    }

    public override string ToString()
    {
        return $"ID: {ID}, Tên: {Name}, Mục tiêu: {Target}, Ngày bắt đầu: {StartDate.ToString("dd/MM/yyyy")}, Ngày kết thúc: {EndDate.ToString("dd/MM/yyyy")}, Trạng thái: {Status}";
    }
}

class Task
{
    public string ID { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Progress { get; set; }

    public Task(string id, string name, DateTime startDate, DateTime endDate, int progress)
    {
        ID = id;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Progress = progress;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Tên: {Name}, Ngày bắt đầu: {StartDate.ToString("dd/MM/yyyy")}, Ngày kết thúc: {EndDate.ToString("dd/MM/yyyy")}, Tiến độ: {Progress}%";
    }
}

class Employee
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
    public int Age { get; set; }

    public Employee(string id, string name, string email, bool isAdmin, int age)
    {
        ID = id;
        Name = name;
        Email = email;
        IsAdmin = isAdmin;
        Age = age;
    }
}

class Meeting
{
    public DateTime Date { get; set; }
    public List<string> Participants { get; set; }
    public string Agenda { get; set; }

    public Meeting(DateTime date, List<string> participants, string agenda)
    {
        Date = date;
        Participants = participants;
        Agenda = agenda;
    }
}
