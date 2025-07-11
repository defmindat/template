﻿namespace EatWise.Users.Infrastructure.Identity;
internal sealed class KeyCloakOptions
{
    public string AdminUrl { get; set; }
    public string TokenUrl { get; set; }
    public string ConfidentialClientId { get; set; }
    public string ConfidentialClientSecret { get; set; }
    public string ClientId { get; set; }
}
