using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nome do Vendedor")]
        [Required(ErrorMessage = "O campo nome é obrigatório")] // Campo obrigatorio
        [StringLength(300, ErrorMessage = "field:{0} Nome deve conter no máximo 300 caracteres.")] // Tamanho do campo
        public string Name { get; set; }

        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="{0} esta inválido, verifique!")]
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo data de nascimento é obrigatório")]
        //[DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Salario Base" ) ]
        [Required(ErrorMessage = "O campo salario é obrigatório")]
        //[DisplayFormat(DataFormatString = "{0:F2}")]
        [DataType(DataType.Currency)]
        //[Range(100.0,100000.00, ErrorMessage ="O salario deve ser entre {1} e {2}")]
        public double BaseSalary { get; set; }
        [DisplayName("Departamento")]
        public Department Department { get; set; }
        [Required(ErrorMessage = "O departamento não foi fornecido")]
        [DisplayName("Id do Departamento")]
   
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthdate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthdate = birthdate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public Seller(string name, string email, DateTime birthdate, double baseSalary, Department department)
        {
            Name = name;
            Email = email;
            Birthdate = birthdate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales // Exemplo de expressão lambda
                .Where(salesRecord => salesRecord.Date >= initial && salesRecord.Date <= final)
                .Sum(salesRecord => salesRecord.Amount);
        }
    }
}
