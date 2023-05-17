﻿namespace WMS.Data.Entity.Identity;

public class UserActivity
{
    public Guid Id { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string? ActivityDescription { get; set; }
    public DateTime ActivityDate { get; set; }
}