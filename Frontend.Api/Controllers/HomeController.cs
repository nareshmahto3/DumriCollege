using Frontend.Api.DbEntities;
using Frontend.Api.DTOs;
using Frontend.Api.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactRepository _contactRepository;

    public ContactController(ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Contact([FromBody] ContactDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var enquiryId = $"ENQ-{DateTime.Now:yyyyMMddHHmmss}";

        // ✅ Save to DB
        var contact = new Contact
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            EnquiryId = enquiryId,
            PhoneNumber = model.PhoneNumber,
            Subject = model.Subject,
            Message = model.Message,
        };

        await _contactRepository.AddAsync(contact);

        try
        {
            // ===========================
            // 📩 1. Email to USER
            // ===========================
            var userEmail = new MimeMessage();
            userEmail.From.Add(new MailboxAddress("Dumri College", "vivekrajmahto52@gmail.com"));
            userEmail.To.Add(new MailboxAddress("", model.Email));

            userEmail.Subject = "We received your request";

            userEmail.Body = new TextPart("plain")
            {
                Text = $"Dear {model.FirstName},\n\n" +
                       $"Your Enquiry ID is: {enquiryId}\n\n" +
                       $"Your request has been received. It will be resolved shortly.\n\n" +
                       $"Thank you,\nDumri College"
            };

            // ===========================
            // 📩 2. Email to ADMIN
            // ===========================
            var adminEmail = new MimeMessage();
            adminEmail.From.Add(new MailboxAddress("Website Contact", "vivekrajmahto52@gmail.com"));
            adminEmail.To.Add(new MailboxAddress("Admin", "vivekrajmahto52@gmail.com"));

            adminEmail.Subject = "New Contact Form Submission";

            adminEmail.Body = new TextPart("plain")
            {
                Text = $"New Contact Form Submission:\n\n" +
                       $"Name: {model.FirstName} {model.LastName}\n" +
                       $"Email: {model.Email}\n" +
                       $"Enquiry ID: {enquiryId}\n\n" +
                       $"Phone: {model.PhoneNumber}\n" +
                       $"Subject: {model.Subject}\n" +
                       $"Message: {model.Message}"
            };

            // ===========================
            // 📡 Send Emails
            // ===========================
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, false);

                // 👉 Use Gmail + App Password
                await smtp.AuthenticateAsync("vivekrajmahto52@gmail.com", "unla biki pcju apza");

                await smtp.SendAsync(userEmail);
                await smtp.SendAsync(adminEmail);

                await smtp.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Saved but email failed: " + ex.Message);
        }

        return Ok("Saved and emails sent successfully ✅");
    }
}