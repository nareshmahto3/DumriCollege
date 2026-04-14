using System.Threading.Tasks;

namespace Students.Api.Services
{
    public interface IStudentPdfService
    {
        Task<byte[]> GenerateStudentApplicationPdf(int applicationId);
    }
}
