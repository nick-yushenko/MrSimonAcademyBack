using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrSimonAcademy2.Models
{
    // Поля, которые можно передать в представление 
    // в представлении прописать @model MrSimonAcademy2.Models.IndexViewModel
    // ХЗ зачем этот класс
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public DateTime Birthday { get; set; }
    }
}