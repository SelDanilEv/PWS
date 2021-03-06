//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Lab6.Models;
using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;

namespace Lab6
{
    public class WcfDataService1 : EntityFrameworkDataService<WS_DEFEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        [WebGet]
        public IQueryable<Note> ChangeNote(int id, String subject, int note1, int studentId)
        {
            WS_DEFEntities context = this.CurrentDataSource;
            Note note = context.Note.Find(id);
            note.Subj = subject;
            note.StudentId = studentId;
            note.Note1 = note1;
            context.SaveChanges();
            return context.Note;
        }

        [WebGet]
        public IQueryable<Note> InsertNote(String subject, int note1, int studentId)
        {
            Note note = new Note();
            note.Subj = subject;
            note.StudentId = studentId;
            note.Note1 = note1;
            WS_DEFEntities context = this.CurrentDataSource;
            context.Note.Add(note);
            context.SaveChanges();
            return context.Note;
        }

        [WebGet]
        public IQueryable<Student> ChangeStudent(int id, String name)
        {
            WS_DEFEntities context = this.CurrentDataSource;
            Student student = context.Student.Find(id);
            student.Name = name;
            context.SaveChanges();
            return context.Student;
        }

        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        [WebGet]
        public IQueryable<Student> InsertStudent(String name)
        {
            Student student = new Student();
            student.Name = name;
            WS_DEFEntities context = this.CurrentDataSource;
            context.Student.Add(student);
            context.SaveChanges();
            return context.Student;
        }
    }
}
