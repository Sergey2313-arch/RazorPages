using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;

        public IndexModel(ContosoUniversity.Data.ContosoUniversityContext context)
        {
            _context = context;
        }

        public string StudentSort { get; set; }
        public string CourseSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int CurrentPageSize { get; set; }

        public PaginatedList<Enrollment> Enrollment { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int? pageSize)
        {
            CurrentSort = sortOrder;
            StudentSort = String.IsNullOrEmpty(sortOrder) ? "student_desc" : "";
            CourseSort = sortOrder == "Course" ? "course_desc" : "Course";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
            CurrentPageSize = pageSize ?? 5; // Пункт №2: по умолчанию 5

            IQueryable<Enrollment> enrollmentsIQ = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student);

            // 1. Поиск (по фамилии студента ИЛИ названию курса)
            if (!String.IsNullOrEmpty(searchString))
            {
                enrollmentsIQ = enrollmentsIQ.Where(e => e.Student.LastName.Contains(searchString)
                                                     || e.Course.Title.Contains(searchString));
            }

            // 1. Сортировка
            switch (sortOrder)
            {
                case "student_desc":
                    enrollmentsIQ = enrollmentsIQ.OrderByDescending(e => e.Student.LastName);
                    break;
                case "Course":
                    enrollmentsIQ = enrollmentsIQ.OrderBy(e => e.Course.Title);
                    break;
                case "course_desc":
                    enrollmentsIQ = enrollmentsIQ.OrderByDescending(e => e.Course.Title);
                    break;
                default:
                    enrollmentsIQ = enrollmentsIQ.OrderBy(e => e.Student.LastName);
                    break;
            }

            // 1. Пагинация
            Enrollment = await PaginatedList<Enrollment>.CreateAsync(
                enrollmentsIQ.AsNoTracking(), pageIndex ?? 1, CurrentPageSize);
        }
    }
}