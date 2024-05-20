﻿namespace wepay.Models
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
