﻿namespace WMS.Core.Models.DocumentModels.Currencies
{
    public class CurrencyListViewModel
    {
        public Guid Id { get; set; }
        public Guid? CreatedUserId { get; set; }
        public string? ApplicationUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
        public string? SymbolCode { get; set; }
        public int CurrencyCode { get; set; }
    }
}