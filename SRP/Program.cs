using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRP
{
    class Program
    {
        static void Main(string[] args)
        {
            var appt = new Appointment
            {
                Patient = new Patient
                {
                    Name = "Mick",
                    Email = "info@gmail.com"
                },
                Time = new DateTime(2021, 12, 30, 15, 10, 0)
            };

            Console.WriteLine("SRP (Single Responsability Principle)\n");
            Console.WriteLine("Una clase debe tener una sola razon para cambiar");
            Console.WriteLine("Una clase debe tener una sola responsabilidad\n");

            Console.WriteLine(new AppointmentService().Create(appt));
        }

        public class AppointmentService
        {
            public string Create(Appointment appointment)
            {
                ValidationResult validation = AppointmentServiceValidator.Validate(appointment);

                return validation.IsValid ?
                    $"Cita agendada al paciente {appointment.Patient.Name}" :
                    string.Join(Environment.NewLine, validation.ErrorMessage);
            }
        }

        public class Patient
        {
            public string Name { get; set; }
            public string Email { get; set; } = "";
        }

        public class Appointment
        {
            public DateTime Time { get; set; }
            public Patient Patient { get; set; }
        }

        public class ValidationResult
        {
            public List<string> ErrorMessage { get; set; } = new List<string>();
            public bool IsValid { get { return !ErrorMessage.Any(); } }
        }

        public static class AppointmentServiceValidator
        {
            public static ValidationResult Validate(Appointment appointment)
            {
                ValidationResult validation = new ValidationResult();
                if (string.IsNullOrEmpty(appointment.Patient.Name))
                {
                    validation.ErrorMessage.Add("Debe proporcionar un nombre");
                }

                if (appointment.Time.Equals(DateTime.MinValue))
                {
                    validation.ErrorMessage.Add("Debe proporcionar una fecha");
                }
            
                if(string.IsNullOrEmpty(appointment.Patient.Email) || !appointment.Patient.Email.Contains("@") )
                {
                    validation.ErrorMessage.Add("Debe proporcionar un email");
                }
                return validation;
            }
        }
    }
}
