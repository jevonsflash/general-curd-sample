using Matoapp.Health.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace Matoapp.Health.Client
{
    public class Client : HealthUser<Guid>, IUser, IUpdateUserData
    {

        //unique

        [StringLength(12)]
        public string ClientNumber { get; set; }

        public string ClientNumberType { get; set; }

        [Range(0.0, 250.0)]
        public double? Height { get; set; }


        [Range(0.0, 1000.0)]
        public double? Weight { get; set; }

        public string Marriage { get; set; }

        public string Status { get; set; }

        protected Client()
        {

        }

        public Client(IUserData user)
            : base(user.Id)
        {
            TenantId = user.TenantId;
            UpdateInternal(user);
        }

        public virtual bool Update(IUserData user)
        {
            if (Id != user.Id)
            {
                throw new ArgumentException($"Given User's Id '{user.Id}' does not match to this User's Id '{Id}'");
            }

            if (TenantId != user.TenantId)
            {
                throw new ArgumentException($"Given User's TenantId '{user.TenantId}' does not match to this User's TenantId '{TenantId}'");
            }

            if (Equals(user))
            {
                return false;
            }

            UpdateInternal(user);
            return true;
        }

        protected virtual bool Equals(IUserData user)
        {
            return Id == user.Id &&
                   TenantId == user.TenantId &&
                   UserName == user.UserName &&
                   Name == user.Name &&
                   Surname == user.Surname &&
                   IsActive == user.IsActive &&
                   Email == user.Email &&
                   EmailConfirmed == user.EmailConfirmed &&
                   PhoneNumber == user.PhoneNumber &&
                   PhoneNumberConfirmed == user.PhoneNumberConfirmed;
        }

        protected virtual void UpdateInternal(IUserData user)
        {
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
            IsActive = user.IsActive;
            EmailConfirmed = user.EmailConfirmed;
            PhoneNumber = user.PhoneNumber;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            UserName = user.UserName;
        }
    }
}
