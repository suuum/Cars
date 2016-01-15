using Projekt1.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Projekt1.v1.Views.Users
{
    public sealed class MailSender
    {

        private MailSender() { }
        private RentACarEntities db = new RentACarEntities();
        private static volatile MailSender instance;
   private static object syncRoot = new Object();

   public static MailSender Instance
   {
      get 
      {
         if (instance == null) 
         {
            lock (syncRoot) 
            {
               if (instance == null) 
				  instance = new MailSender();
            }
         }

         return instance;
      }
   }

   public void SendMail(string userEmail, string mailSubjest)
   {

       string body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
           var message = new MailMessage();
           message.To.Add(userEmail);
           message.From = new MailAddress("andrzej.przykladowy@gmail.com");
           message.Subject = mailSubjest;
           message.Body = body;
           message.IsBodyHtml = true;

           using (var smtp = new SmtpClient()) 
           {
               var credential = new NetworkCredential
            {
                UserName = "andrzej.przykladowy@gmail.com",  // replace with valid value
                Password = "ZtpProjekt"  // replace with valid value
            };
            smtp.Credentials = credential;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
             smtp.Send(message);

           }
   }
   public IEnumerable<User> CheckDateReturnDate() {
   List<User> list = db.User.ToList();
   List<User> listWithDate = new List<User>();
       foreach (User item in list)
	{
        foreach (Borrow borrow in item.Borrow.ToList())
        {
            if (DateTime.Now.CompareTo(borrow.ToDate)<0 && borrow.IsReturned==false) {
                listWithDate.Add(item);
                break;
            }   
        }
 		 
	}
       return listWithDate; 
   }



    }
}