using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Utility.Data.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Students.Api.DbEntities;

namespace Students.Api.Services
{
    public class StudentPdfService : IStudentPdfService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public StudentPdfService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<byte[]> GenerateStudentApplicationPdf(int applicationId)
        {
            var appRepo = _unitOfWork.Repository<StudentApplication>();
            var subjectSelRepo = _unitOfWork.Repository<StudentSubjectSelection>();
            var facultyRepo = _unitOfWork.Repository<Faculty>();
            var compRepo = _unitOfWork.Repository<CompulsorySubject>();
            var optRepo = _unitOfWork.Repository<OptionalSubject>();
            var addRepo = _unitOfWork.Repository<AdditionalSubject>();
            var facultyCompRepo = _unitOfWork.Repository<FacultyCompulsorySubject>();
            var examRepo = _unitOfWork.Repository<StudentExamDetail>();
            var certRepo = _unitOfWork.Repository<StudentCertificate>();

            var applications = await appRepo.GetAllAsync();
            var application = applications.FirstOrDefault(a => a.ApplicationId == applicationId);
            if (application == null)
                throw new InvalidOperationException("Application not found.");

            // Ensure navigation properties for master tables are populated from IDs
            if (application.ReligionId.HasValue)
            {
                var relRepo = _unitOfWork.Repository<Religion>();
                application.Religion = await relRepo.GetByIdAsync(application.ReligionId.Value);
            }

            if (application.CasteId.HasValue)
            {
                var casteRepo = _unitOfWork.Repository<Caste>();
                application.Caste = await casteRepo.GetByIdAsync(application.CasteId.Value);
            }

            if (application.GenderId.HasValue)
            {
                var genderRepo = _unitOfWork.Repository<Gender>();
                application.Gender = await genderRepo.GetByIdAsync(application.GenderId.Value);
            }

            if (application.CategoryId.HasValue)
            {
                var catRepo = _unitOfWork.Repository<Category>();
                application.Category = await catRepo.GetByIdAsync(application.CategoryId.Value);
            }

            if (application.BloodGroupId.HasValue)
            {
                var bgRepo = _unitOfWork.Repository<BloodGroup>();
                application.BloodGroup = await bgRepo.GetByIdAsync(application.BloodGroupId.Value);
            }

            if (application.MaritalStatusId.HasValue)
            {
                var msRepo = _unitOfWork.Repository<MaritalStatus>();
                application.MaritalStatus = await msRepo.GetByIdAsync(application.MaritalStatusId.Value);
            }

            var subjectSelections = await subjectSelRepo.GetAllAsync();
            var subjectSelection = subjectSelections.FirstOrDefault(s => s.ApplicationId == applicationId);

            Faculty? faculty = null;
            CompulsorySubject? compSubject = null;
            OptionalSubject? opt1 = null;
            OptionalSubject? opt2 = null;
            OptionalSubject? opt3 = null;
            AdditionalSubject? addSubject = null;

            if (subjectSelection != null)
            {
                if (subjectSelection.FacultyId.HasValue)
                    faculty = await facultyRepo.GetByIdAsync(subjectSelection.FacultyId.Value);

                if (subjectSelection.CompulsorySubjectId.HasValue)
                    compSubject = await compRepo.GetByIdAsync(subjectSelection.CompulsorySubjectId.Value);

                if (subjectSelection.OptionalSubject1.HasValue)
                    opt1 = await optRepo.GetByIdAsync(subjectSelection.OptionalSubject1.Value);

                if (subjectSelection.OptionalSubject2.HasValue)
                    opt2 = await optRepo.GetByIdAsync(subjectSelection.OptionalSubject2.Value);

                if (subjectSelection.OptionalSubject3.HasValue)
                    opt3 = await optRepo.GetByIdAsync(subjectSelection.OptionalSubject3.Value);

                if (subjectSelection.AdditionalSubjectId.HasValue)
                    addSubject = await addRepo.GetByIdAsync(subjectSelection.AdditionalSubjectId.Value);
            }

            var examDetails = (await examRepo.GetAllAsync())
                .Where(e => e.ApplicationId == applicationId)
                .OrderBy(e => e.YearOfPassing)
                .ToList();

            var certificates = (await certRepo.GetAllAsync())
                .Where(c => c.ApplicationId == applicationId)
                .ToList();

            var languageCompulsory = compSubject?.SubjectName ?? string.Empty;

            var facultyCompAll = await facultyCompRepo.GetAllAsync();
            var facultyCompulsories = facultyCompAll
                .Where(fc => fc.FacultyId == subjectSelection?.FacultyId)
                .Select(fc => fc.SubjectName)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            var compSubjectsDisplay = facultyCompulsories.Any()
                ? string.Join(", ", facultyCompulsories)
                : string.Empty;

            var photoCert = certificates
                .FirstOrDefault(c => c.CertificateType != null &&
                                     c.CertificateType.Equals("Profile Photo", StringComparison.OrdinalIgnoreCase));

            byte[]? photoBytes = null;
            if (photoCert != null && !string.IsNullOrWhiteSpace(photoCert.FilePath))
            {
                var root = _env.WebRootPath ?? _env.ContentRootPath ?? Directory.GetCurrentDirectory();
                var fullPath = Path.Combine(root, photoCert.FilePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

                if (File.Exists(fullPath))
                    photoBytes = await File.ReadAllBytesAsync(fullPath);
            }

            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        // HEADER: Dumri College logo + address + contact
                        col.Item().ShowEntire().Border(0.5f).Padding(4).Row(row =>
                        {
                            // Left: simple logo box
                            row.ConstantItem(70).Column(left =>
                            {
                                left.Item().Border(0.5f).Padding(4)
                                    .AlignCenter().AlignMiddle()
                                    .Text("LOGO").FontSize(10).Bold();
                            });

                            // Center: college name, form title, address, phone, email
                            row.RelativeItem().Column(center =>
                            {
                                center.Item().AlignCenter()
                                    .Text("DUMRI COLLEGE DUMRI")
                                    .Bold().FontSize(16);

                                center.Item().AlignCenter()
                                    .Text("GOVERNMENT STUDENT ADMISSION FORM")
                                    .Bold().FontSize(12);

                                center.Item().PaddingTop(2).AlignCenter()
                                      .Text("At+Po: Dumri, Dist: Giridih, Jharkhand - 825106")
                                      .FontSize(9);

                                center.Item().AlignCenter()
                                      .Text("Phone: 0000-000000, 1111-111111  |  Email: info@dumricollege.gov.in")
                                      .FontSize(9);
                            });

                            // Right: Registration No, Application ID and Date
                            row.ConstantItem(220).Column(right =>
                            {
                                right.Item().AlignLeft().Text(text =>
                            {
                                    text.Span("Registration No: ").Bold();
                                    text.Span(application.RegistrationNo ?? string.Empty);
                                });

                                right.Item().AlignLeft().Text(text =>
                                {
                                    text.Span("Application ID: ").Bold();
                                    text.Span(application.ApplicationId.ToString());
                                });

                                right.Item().AlignLeft().Text(text =>
                                {
                                    text.Span("Date: ").Bold();
                                    text.Span(DateTime.Now.ToString("dd-MM-yyyy"));
                                });
                            });
                        });

                        // PERSONAL INFORMATION GRID (unchanged except it uses navigation names)
                        col.Item().PaddingTop(6).Border(0.5f).Padding(4).Column(section =>
                        {
                            section.Item().Text("1. Personal Information").Bold().Underline();

                            section.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2); // Label
                                    columns.RelativeColumn(4); // Value
                                    columns.RelativeColumn(2); // Photo (spans rows)
                                });

                                void InfoRow(string label, string? value, bool includePhotoCell)
                                {
                                    table.Cell().Border(0.5f).Padding(4).Text(label);
                                    table.Cell().Border(0.5f).Padding(4).Text(value ?? string.Empty);

                                    if (includePhotoCell)
                                    {
                                        const int totalRowsAfterFirst = 15; // keep same span

                                        table.Cell().RowSpan(totalRowsAfterFirst)
                                            .Border(0.5f).Padding(4).Column(p =>
                                            {
                                                p.Item().AlignCenter().Text("Student Photo").FontSize(9);
                                                p.Item().Element(e =>
                                                {
                                                    if (photoBytes != null)
                                                    {
                                                        e.Border(0.5f).Padding(2).Height(110).Width(90)
                                                         .Image(photoBytes).FitArea();
                                                    }
                                                    else
                                                    {
                                                        e.Border(0.5f).Padding(2).Height(110).Width(90)
                                                         .AlignCenter().AlignMiddle()
                                                         .Text("No Photo").FontSize(9);
                                                    }
                                                });
                                            });
                                    }
                                }

                                // Read display values from navigation properties (loaded above)
                                var genderName = application.Gender?.GenderName ?? string.Empty;
                                var religionName = application.Religion?.ReligionName ?? string.Empty;
                                var casteName = application.Caste?.CasteName ?? string.Empty;
                                var categoryName = application.Category?.CategoryName ?? string.Empty;
                                var bloodGroupName = application.BloodGroup?.BloodGroupName ?? string.Empty;
                                var maritalStatusName = application.MaritalStatus?.MaritalStatusName ?? string.Empty;

                                InfoRow("Student Name", application.StudentName, true);
                                InfoRow("Father Name", application.FatherName, false);
                                InfoRow("Mother Name", application.MotherName, false);
                                InfoRow("Date of Birth", application.DateOfBirth?.ToString("dd-MM-yyyy"), false);
                                InfoRow("Gender", genderName, false);
                                InfoRow("Religion", religionName, false);
                                InfoRow("Nationality", application.Nationality, false);
                                InfoRow("Caste", casteName, false);
                                InfoRow("Category", categoryName, false);
                                InfoRow("Blood Group", bloodGroupName, false);
                                InfoRow("Identification Mark", application.IdentificationMark, false);
                                InfoRow("Marital Status", maritalStatusName, false);
                                InfoRow("Aadhaar Number", application.AadhaarNumber, false);
                                InfoRow("Mobile Number", application.MobileNumber, false);
                                InfoRow("Height", application.Height, false);
                                InfoRow("Weight", application.Weight, false);
                            });
                        });

                        // ADDRESS SECTION, FACULTY & SUBJECTS, EXAM, DOCUMENTS, DECLARATION, SIGNATURES
                        // remain exactly as already in your file (omitted here for brevity).

                        col.Item().PaddingTop(6).Border(0.5f).Padding(4).Column(section2 =>
                        {
                            section2.Item().Text("2. Address Details").Bold().Underline();

                            section2.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(4);
                                });

                                void AddrRow(string label, string? value)
                                {
                                    table.Cell().Border(0.5f).Padding(4).Text(label);
                                    table.Cell().Border(0.5f).Padding(4).Text(value ?? string.Empty);
                                }

                                AddrRow("Permanent Address", application.PermanentAddress);
                                AddrRow("Local Address", application.LocalAddress);
                            });
                        });

                        // FACULTY AND SUBJECTS SECTION
                        col.Item().PaddingTop(6).Border(0.5f).Padding(4).Column(section =>
                        {
                            section.Item().Text("3. Faculty and Subjects").Bold().Underline();

                            var facultyName = faculty?.FacultyName ?? string.Empty;
                            var compName = compSubjectsDisplay;
                            var opt1Name = opt1?.SubjectName ?? string.Empty;
                            var opt2Name = opt2?.SubjectName ?? string.Empty;
                            var opt3Name = opt3?.SubjectName ?? string.Empty;
                            var addName = addSubject?.SubjectName ?? string.Empty;

                            section.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(4);
                                });

                                void SubRow(string label, string value)
                                {
                                    table.Cell().Border(0.5f).Padding(4).Text(label);
                                    table.Cell().Border(0.5f).Padding(4).Text(value);
                                }

                                SubRow("Faculty", facultyName);
                                SubRow("Mother Tongue Subject", languageCompulsory);
                                SubRow("Faculty Compulsory Subjects", compName);
                                SubRow("Optional Subject 1", opt1Name);
                                SubRow("Optional Subject 2", opt2Name);
                                SubRow("Optional Subject 3", opt3Name);
                                SubRow("Additional Subject", addName);
                            });
                        });

                        // EXAM DETAILS SECTION
                        col.Item().PaddingTop(6).Border(0.5f).Padding(4).Column(section =>
                        {
                            section.Item().Text("4. Exam Details").Bold().Underline();

                            section.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2); // School / College
                                    columns.RelativeColumn(2); // Board / Council
                                    columns.RelativeColumn(2); // Exam Name
                                    columns.RelativeColumn(1); // Year Of Passing
                                    columns.RelativeColumn(1); // Division / Rank
                                    columns.RelativeColumn(2); // Subjects
                                });

                                table.Header(header =>
                                {
                                    void HeaderCell(string label)
                                    {
                                        header.Cell().Element(c => c.Background(Colors.Grey.Lighten3)
                                                                 .Border(0.5f)
                                                                 .Padding(4))
                                              .Text(label).SemiBold();
                                    }

                                    HeaderCell("School / College");
                                    HeaderCell("Board / Council");
                                    HeaderCell("Exam Name");
                                    HeaderCell("Year Of Passing");
                                    HeaderCell("Division / Rank");
                                    HeaderCell("Subjects");
                                });

                                foreach (var e in examDetails)
                                {
                                    table.Cell().Border(0.5f).Padding(4)
                                        .Text(e.SchoolCollege ?? string.Empty);
                                    table.Cell().Border(0.5f).Padding(4)
                                        .Text(e.BoardCouncil ?? string.Empty);
                                    table.Cell().Border(0.5f).Padding(4)
                                        .Text(e.ExamName ?? string.Empty);
                                    table.Cell().Border(0.5f).Padding(4)
                                        .Text(e.YearOfPassing?.ToString() ?? string.Empty);
                                    table.Cell().Border(0.5f).Padding(4)
                                        .Text(e.DivisionOrRank ?? string.Empty);
                                    table.Cell().Border(0.5f).Padding(4)
                                        .Text(e.Subjects ?? string.Empty);
                                }
                            });
                        });

                        // UPLOADED DOCUMENTS SECTION
                        col.Item().PaddingTop(6).Border(0.5f).Padding(4).Column(section =>
                        {
                            section.Item().Text("5. Uploaded Documents").Bold().Underline();

                            string? GetDocPath(string type) =>
                                certificates
                                    .FirstOrDefault(c => c.CertificateType != null &&
                                                         c.CertificateType.Equals(type, StringComparison.OrdinalIgnoreCase))
                                    ?.FilePath;

                            section.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2); // Label
                                    columns.RelativeColumn(4); // File name box
                                });

                                void DocRow(string label, string? path)
                                {
                                    var display = string.IsNullOrWhiteSpace(path)
                                        ? "Not Uploaded"
                                        : Path.GetFileName(path);

                                    table.Cell().Border(0.5f).Padding(4).Text(label);
                                    table.Cell().Border(0.5f).Padding(4).Text(display);
                                }

                                DocRow("Caste Certificate", GetDocPath("Caste Certificate"));
                                DocRow("School Leaving Certificate", GetDocPath("School Leaving Certificate"));
                                DocRow("Admit Card", GetDocPath("Admit Card"));
                                DocRow("Marksheet", GetDocPath("Marksheet"));
                                DocRow("Aadhaar Card", GetDocPath("Aadhaar Card"));
                            });
                        });

                        // DECLARATION
                        col.Item().PaddingTop(6).Border(0.5f).Padding(4).Column(section =>
                        {
                            section.Item().Text("6. Declaration").Bold().Underline();

                            section.Item().PaddingTop(4).Text(
                                "I hereby declare that the information provided above is true and correct to the best of my knowledge and belief.")
                                .FontSize(10);
                        });

                        // SIGNATURES
                        col.Item().PaddingTop(10).Border(0.5f).Padding(4).Row(row =>
                        {
                            row.RelativeItem(1).Column(c2 =>
                            {
                                c2.Item().Text("Student Signature").AlignLeft();
                                c2.Item().Text($"Date: {DateTime.Now:dd-MM-yyyy}")
                                    .AlignLeft().FontSize(9);
                            });

                            row.RelativeItem(1).AlignRight().Column(c2 =>
                            {
                                c2.Item().Text("Authority Signature").AlignRight();
                            });
                        });
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Generated by Students.Api - ").FontSize(8);
                        x.Span(DateTime.Now.ToString("dd-MM-yyyy HH:mm")).FontSize(8);
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}