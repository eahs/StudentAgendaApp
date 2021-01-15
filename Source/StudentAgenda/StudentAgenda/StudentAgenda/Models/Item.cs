using System;

namespace StudentAgenda.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Time { get; internal set; }
        public string Materials { get; internal set; }
        public string Date { get; internal set; }
        public string Subject { get; internal set; }
        public string Difficulty { get; internal set; }
    }
}