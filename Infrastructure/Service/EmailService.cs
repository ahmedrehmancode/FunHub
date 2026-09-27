using Application.Common;
using Domain.Exceptions;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Text;
//using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Application.Interface.Servies;

namespace Infrastructure.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailConfig> emailconfig, ILogger<EmailService> logger)
        {
            _emailConfig = emailconfig.Value;
            _logger = logger;
        }
        public async Task<bool> SendVerificationEmailAsync(string email, string verificationLink)
        {
            try
            {
                _logger.LogInformation($"Email Sender : Sending verification email to {email}...");
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));

                message.To.Add(new MailboxAddress("", email));

                message.Subject = "Email Verification";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                              <!DOCTYPE html>
                              <html>
                              <head>
                                  <meta charset='UTF-8'>
                                  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                  <title>Verify Your FanHub Email</title>
                              </head>
                              
                              <body style='
                                  margin: 0;
                                  padding: 0;
                                  background-color: #ffffff;
                                  font-family: Arial, Helvetica, sans-serif;
                                  color: #6b6375;
                              '>
                              
                                  <table width='100%' cellpadding='0' cellspacing='0' border='0'
                                         style='
                                             background-color: #ffffff;
                                             padding: 40px 15px;
                                         '>
                              
                                      <tr>
                                          <td align='center'>
                              
                                              <!-- Main Container -->
                                              <table width='600' cellpadding='0' cellspacing='0' border='0'
                                                     style='
                                                         max-width: 600px;
                                                         width: 100%;
                                                         background-color: #ffffff;
                                                         border: 1px solid #e5e4e7;
                                                         border-radius: 14px;
                                                         overflow: hidden;
                                                         box-shadow:
                                                             0 10px 15px -3px rgba(0,0,0,0.1),
                                                             0 4px 6px -2px rgba(0,0,0,0.05);
                                                     '>
                              
                                                  <!-- Header -->
                                                  <tr>
                                                      <td align='center'
                                                          style='
                                                              padding: 35px 20px;
                                                              border-bottom: 1px solid #e5e4e7;
                                                          '>
                              
                                                          <div style='
                                                              font-size: 32px;
                                                              font-weight: bold;
                                                              color: #08060d;
                                                              letter-spacing: -1px;
                                                          '>
                                                              Fan<span style='color: #aa3bff;'>Hub</span>
                                                          </div>
                              
                                                          <div style='
                                                              margin-top: 8px;
                                                              font-size: 13px;
                                                              color: #6b6375;
                                                          '>
                                                              Movies • Anime • Entertainment
                                                          </div>
                              
                                                      </td>
                                                  </tr>
                              
                              
                                                  <!-- Content -->
                                                  <tr>
                                                      <td style='padding: 40px;'>
                              
                                                          <h1 style='
                                                              margin: 0 0 20px 0;
                                                              text-align: center;
                                                              font-size: 27px;
                                                              color: #08060d;
                                                          '>
                                                              Verify Your Email
                                                          </h1>
                              
                                                          <p style='
                                                              margin: 0 0 16px 0;
                                                              font-size: 15px;
                                                              line-height: 1.7;
                                                              color: #6b6375;
                                                          '>
                                                              Welcome to
                                                              <strong style='color: #08060d;'>
                                                                  FanHub
                                                              </strong>!
                                                          </p>
                              
                                                          <p style='
                                                              margin: 0 0 28px 0;
                                                              font-size: 15px;
                                                              line-height: 1.7;
                                                              color: #6b6375;
                                                          '>
                                                              You're one step away from joining the FanHub
                                                              community. Please verify your email address
                                                              to activate your account and start exploring
                                                              movies, anime, and more.
                                                          </p>
                              
                              
                                                          <!-- Verify Button -->
                                                          <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                                                              <tr>
                                                                  <td align='center'>
                              
                                                                      <a href='{verificationLink}'
                                                                         style='
                                                                             display: inline-block;
                                                                             background-color: #aa3bff;
                                                                             color: #ffffff;
                                                                             text-decoration: none;
                                                                             font-size: 15px;
                                                                             font-weight: bold;
                                                                             padding: 14px 32px;
                                                                             border-radius: 8px;
                                                                         '>
                                                                          Verify My Email
                                                                      </a>
                              
                                                                  </td>
                                                              </tr>
                                                          </table>
                              
                              
                                                          <!-- Fallback Link -->
                                                          <p style='
                                                              margin: 30px 0 10px 0;
                                                              font-size: 13px;
                                                              color: #6b6375;
                                                          '>
                                                              If the button doesn't work, copy and paste
                                                              the following link into your browser:
                                                          </p>
                              
                                                          <div style='
                                                              padding: 14px;
                                                              background-color: #f4f3ec;
                                                              border: 1px solid #e5e4e7;
                                                              border-radius: 7px;
                                                              word-break: break-all;
                                                              font-size: 12px;
                                                              line-height: 1.6;
                                                              color: #6b6375;
                                                          '>
                                                              {verificationLink}
                                                          </div>
                              
                              
                                                          <!-- Security Notice -->
                                                          <table width='100%' cellpadding='0' cellspacing='0' border='0'
                                                                 style='margin-top: 30px;'>
                              
                                                              <tr>
                                                                  <td style='
                                                                      background-color: rgba(170, 59, 255, 0.1);
                                                                      border-left: 4px solid #aa3bff;
                                                                      padding: 15px;
                                                                      border-radius: 5px;
                                                                  '>
                              
                                                                      <p style='
                                                                          margin: 0;
                                                                          font-size: 13px;
                                                                          line-height: 1.6;
                                                                          color: #6b6375;
                                                                      '>
                                                                          <strong style='color: #08060d;'>
                                                                              Security notice:
                                                                          </strong>
                                                                          If you did not create a FanHub
                                                                          account, you can safely ignore
                                                                          this email.
                                                                      </p>
                              
                                                                  </td>
                                                              </tr>
                              
                                                          </table>
                              
                                                      </td>
                                                  </tr>
                              
                              
                                                  <!-- Footer -->
                                                  <tr>
                                                      <td align='center'
                                                          style='
                                                              padding: 25px 20px;
                                                              background-color: rgba(244, 243, 236, 0.5);
                                                              border-top: 1px solid #e5e4e7;
                                                          '>
                              
                                                          <div style='
                                                              font-size: 18px;
                                                              font-weight: bold;
                                                              color: #08060d;
                                                          '>
                                                              Fan<span style='color: #aa3bff;'>Hub</span>
                                                          </div>
                              
                                                          <p style='
                                                              margin: 8px 0;
                                                              font-size: 12px;
                                                              color: #6b6375;
                                                          '>
                                                              Movies • Anime • Entertainment
                                                          </p>
                              
                                                          <p style='
                                                              margin: 0;
                                                              font-size: 11px;
                                                              color: #6b6375;
                                                          '>
                                                              This is an automated email. Please do not reply.
                                                          </p>
                              
                                                          <p style='
                                                              margin: 10px 0 0 0;
                                                              font-size: 11px;
                                                              color: #6b6375;
                                                          '>
                                                              © 2026 FanHub. All rights reserved.
                                                          </p>
                              
                                                      </td>
                                                  </tr>
                              
                                              </table>
                              
                                          </td>
                                      </tr>
                              
                                  </table>
                              
                              </body>
                              </html>"
                };

                _logger.LogInformation($"Email Sender : Connecting to SMTP server {_emailConfig.SmtpServer}:{_emailConfig.Port}...");

                using var client = new SmtpClient();

                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                _logger.LogInformation($"Email Sender : Connected to SMTP server {_emailConfig.SmtpServer}:{_emailConfig.Port}.");

                _logger.LogInformation($"DEBUG - Username: [{_emailConfig.Username}], PasswordLength: {_emailConfig.Password?.Length}");

                await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password!);


                await client.SendAsync(message);

                await client.DisconnectAsync(true);


                _logger.LogInformation($"Email Sender : Verification email sent to {email} successfully.");

                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Wen't Wrong : {ex.Message}");
                throw new AppException("Something Wen't Wrong");

            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email, string resetLink)
        {
            try
            {
                _logger.LogInformation($"Email Sender : Sending password reset email to {email}...");
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Reset Your Password";

                message.Body = new TextPart("html")
                {
                    Text = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Reset Your FanHub Password</title>
</head>

<body style='
    margin: 0;
    padding: 0;
    background-color: #ffffff;
    font-family: Arial, Helvetica, sans-serif;
    color: #6b6375;
'>

    <table width='100%' cellpadding='0' cellspacing='0' border='0'
           style='
               background-color: #ffffff;
               padding: 40px 15px;
           '>

        <tr>
            <td align='center'>

                <table width='600' cellpadding='0' cellspacing='0' border='0'
                       style='
                           max-width: 600px;
                           width: 100%;
                           background-color: #ffffff;
                           border: 1px solid #e5e4e7;
                           border-radius: 14px;
                           overflow: hidden;
                           box-shadow:
                               0 10px 15px -3px rgba(0,0,0,0.1),
                               0 4px 6px -2px rgba(0,0,0,0.05);
                       '>

                    <tr>
                        <td align='center'
                            style='
                                padding: 35px 20px;
                                border-bottom: 1px solid #e5e4e7;
                            '>

                            <div style='
                                font-size: 32px;
                                font-weight: bold;
                                color: #08060d;
                                letter-spacing: -1px;
                            '>
                                Fan<span style='color: #aa3bff;'>Hub</span>
                            </div>

                            <div style='
                                margin-top: 8px;
                                font-size: 13px;
                                color: #6b6375;
                            '>
                                Movies • Anime • Entertainment
                            </div>

                        </td>
                    </tr>

                    <tr>
                        <td style='padding: 40px;'>

                            <h1 style='
                                margin: 0 0 20px 0;
                                text-align: center;
                                font-size: 27px;
                                color: #08060d;
                            '>
                                Reset Your Password
                            </h1>

                            <p style='
                                margin: 0 0 16px 0;
                                font-size: 15px;
                                line-height: 1.7;
                                color: #6b6375;
                            '>
                                Hello from
                                <strong style='color: #08060d;'>
                                    FanHub
                                </strong>!
                            </p>

                            <p style='
                                margin: 0 0 28px 0;
                                font-size: 15px;
                                line-height: 1.7;
                                color: #6b6375;
                            '>
                                We received a request to reset your password.
                                Click the button below to set a new password.
                                This link will expire in 30 minutes.
                            </p>

                            <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                                <tr>
                                    <td align='center'>

                                        <a href='{resetLink}'
                                           style='
                                               display: inline-block;
                                               background-color: #aa3bff;
                                               color: #ffffff;
                                               text-decoration: none;
                                               font-size: 15px;
                                               font-weight: bold;
                                               padding: 14px 32px;
                                               border-radius: 8px;
                                           '>
                                            Reset My Password
                                        </a>

                                    </td>
                                </tr>
                            </table>

                            <p style='
                                margin: 30px 0 10px 0;
                                font-size: 13px;
                                color: #6b6375;
                            '>
                                If the button doesn't work, copy and paste
                                the following link into your browser:
                            </p>

                            <div style='
                                padding: 14px;
                                background-color: #f4f3ec;
                                border: 1px solid #e5e4e7;
                                border-radius: 7px;
                                word-break: break-all;
                                font-size: 12px;
                                line-height: 1.6;
                                color: #6b6375;
                            '>
                                {resetLink}
                            </div>

                            <table width='100%' cellpadding='0' cellspacing='0' border='0'
                                   style='margin-top: 30px;'>

                                <tr>
                                    <td style='
                                        background-color: rgba(170, 59, 255, 0.1);
                                        border-left: 4px solid #aa3bff;
                                        padding: 15px;
                                        border-radius: 5px;
                                    '>

                                        <p style='
                                            margin: 0;
                                            font-size: 13px;
                                            line-height: 1.6;
                                            color: #6b6375;
                                        '>
                                            <strong style='color: #08060d;'>
                                                Security notice:
                                            </strong>
                                            If you did not request a password
                                            reset, you can safely ignore this
                                            email. Your password will remain
                                            unchanged.
                                        </p>

                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>

                    <tr>
                        <td align='center'
                            style='
                                padding: 25px 20px;
                                background-color: rgba(244, 243, 236, 0.5);
                                border-top: 1px solid #e5e4e7;
                            '>

                            <div style='
                                font-size: 18px;
                                font-weight: bold;
                                color: #08060d;
                            '>
                                Fan<span style='color: #aa3bff;'>Hub</span>
                            </div>

                            <p style='
                                margin: 8px 0;
                                font-size: 12px;
                                color: #6b6375;
                            '>k
                            '>k
                                Movies • Anime • Entertainment
                            </p>

                            <p style='
                                margin: 0;
                                font-size: 11px;
                                color: #6b6375;
                            '>
                                This is an automated email. Please do not reply.
                            </p>

                            <p style='
                                margin: 10px 0 0 0;
                                font-size: 11px;
                                color: #6b6375;
                            '>
                                © 2026 FanHub. All rights reserved.
                            </p>

                        </td>
                    </tr>

                </table>

            </td>
        </tr>

    </table>

</body>
</html>"
                };

                _logger.LogInformation($"Email Sender : Connecting to SMTP server {_emailConfig.SmtpServer}:{_emailConfig.Port}...");

                using var client = new SmtpClient();

                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                _logger.LogInformation($"Email Sender : Connected to SMTP server {_emailConfig.SmtpServer}:{_emailConfig.Port}.");

                await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);

                _logger.LogInformation($"Email Sender : Password reset email sent to {email} successfully.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Wen't Wrong : {ex.Message}");
                throw new AppException("Something Wen't Wrong");
            }
        }
    }
}
