using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DXApplication2.Module.BusinessObjects;

namespace DXApplication2.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            if (ObjectSpace.CanInstantiate(typeof(Person)))
            {
                Person person = ObjectSpace.FirstOrDefault<Person>(p => p.FirstName == "John" && p.LastName == "Nilsen");
                if (person == null)
                {
                    person = ObjectSpace.CreateObject<Person>();
                    person.FirstName = "John";
                    person.LastName = "Nilsen";
                }
            }
            if (ObjectSpace.CanInstantiate(typeof(Warehouse)))
            {
                Warehouse warehouse = ObjectSpace.FirstOrDefault<Warehouse>(p => p.Name == "Склад 1");
                if (warehouse == null)
                {
                    warehouse = ObjectSpace.CreateObject<Warehouse>();
                    warehouse.Name = "Склад 1";
                    warehouse.Manager = ObjectSpace.FirstOrDefault<Person>(p => p.FirstName == "John" && p.LastName == "Nilsen");
                    //warehouse.Pickets.Add(ObjectSpace.FirstOrDefault<Picket>(t => t.Number == 101));
                }
            }

            if (ObjectSpace.CanInstantiate(typeof(Picket)))
            {
                Picket picket = ObjectSpace.FirstOrDefault<Picket>(t => t.Number == 101);
                if (picket == null)
                {
                    picket = ObjectSpace.CreateObject<Picket>();
                    picket.Number = 101;
                    picket.Platform = ObjectSpace.FirstOrDefault<Platform>(p => p.Name == "101-102");
                    //picket.Warehouse = ObjectSpace.FirstOrDefault<Warehouse>(w => w.Name == "Cклад 1");
                }
            }

            if (ObjectSpace.CanInstantiate(typeof(Platform)))
            {
                Platform platform = ObjectSpace.FirstOrDefault<Platform>(p => p.Name == "101-102");
                if (platform == null)
                {
                    platform = ObjectSpace.CreateObject<Platform>();
                    platform.Name = "101-102";
                    platform.Pickets.Add(ObjectSpace.FirstOrDefault<Picket>(t => t.Platform.Name == "101-102"));
                    platform.Warehouse = ObjectSpace.FirstOrDefault<Warehouse>(w => w.Name == "Склад 1");
                }
            }
            PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == SecurityStrategy.AdministratorRoleName);
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
                adminRole.AddMemberPermissionFromLambda<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow); //
                adminRole.AddMemberPermissionFromLambda<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow); //
                adminRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);

            }
            PermissionPolicyRole userRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Users");
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "Users";
                userRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess,
        SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyUser>(SecurityOperations.FullAccess, //
        SecurityPermissionState.Deny);
                userRole.AddObjectPermissionFromLambda<PermissionPolicyUser>(SecurityOperations.ReadOnlyAccess, //
        u => u.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, //
        "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, //
        "StoredPassword", null, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create",
        SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create",
        SecurityPermissionState.Deny);
            }

            // ...
            // If a user named 'Sam' does not exist in the database, create this user.
            PermissionPolicyUser user1 = ObjectSpace.FirstOrDefault<PermissionPolicyUser>(user => user.UserName == "Sam");
            if (user1 == null)
            {
                user1 = ObjectSpace.CreateObject<PermissionPolicyUser>();
                user1.UserName = "Sam";
                // Set a password if the standard authentication type is used.
                user1.SetPassword("admin");
            }
            // If a user named 'John' does not exist in the database, create this user.
            PermissionPolicyUser user2 = ObjectSpace.FirstOrDefault<PermissionPolicyUser>(user => user.UserName == "John");
            if (user2 == null)
            {
                user2 = ObjectSpace.CreateObject<PermissionPolicyUser>(); //
                user2.UserName = "John";
                // Set a password if the standard authentication type is used.
                user2.SetPassword("user");
            }

            user1.Roles.Add(adminRole);
            user2.Roles.Add(userRole);

            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
