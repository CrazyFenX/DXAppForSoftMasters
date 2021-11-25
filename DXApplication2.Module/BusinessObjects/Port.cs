using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication2.Module.BusinessObjects
{
    /// <summary>
    /// Picket class
    /// </summary>
    [NavigationItem("Port")]
    public class Picket : BaseObject
    {
        public Picket(Session session) : base(session) { }

        /// <summary>
        /// The Picket's number (required and identity field)
        /// </summary>
        int number;
        [RuleRequiredField("RuleRequiredField for Picket.Number",
    DefaultContexts.Save)]
        public int Number
        {
            get { return number; }
            set { SetPropertyValue(nameof(Number), ref number, value); } //method to implement the property’s setter.
        }

        ////     /// <summary>
        ////     /// The Warehouse where the Picket is registered
        ////     /// </summary>
        ////     Warehouse warehouse;
        ////     [Association]
        ////     [RuleRequiredField("RuleRequiredField for Picket.Warehouse",
        ////DefaultContexts.Save)]
        ////     public Warehouse Warehouse
        ////     {
        ////         get { return warehouse; }
        ////         set { SetPropertyValue(nameof(Warehouse), ref warehouse, value); }
        ////     }

        /// <summary>
        /// The Platform where the Picket is located
        /// </summary>
        Platform platform;
        [RuleRequiredField("RuleRequiredField for Picket.Platform",
    DefaultContexts.Save)]
        [Association]
        public Platform Platform
        {
            get { return platform; }
            set { SetPropertyValue(nameof(Platform), ref platform, value); }
        }

        private XPCollection<AuditDataItemPersistent> changeHistory;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> ChangeHistory
        {
            get
            {
                if (changeHistory == null)
                {
                    changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return changeHistory;
            }
        }
    }

    /// <summary>
    /// Warehouse class
    /// </summary>
    [NavigationItem("Port")]
    public class Warehouse : BaseObject
    {
        public Warehouse(Session session) : base(session) { }

        /// <summary>
        /// The name of Warehouse
        /// </summary>
        string name;
        [RuleRequiredField("RuleRequiredField for Warehouse.Name",
    DefaultContexts.Save)]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue(nameof(Name), ref name, value); }
        }

        /// <summary>
        /// The manager who manages the Warehouse
        /// </summary>
        Person manager;
        [RuleRequiredField("RuleRequiredField for Warehouse.Manager",
    DefaultContexts.Save)]
        public Person Manager
        {
            get { return manager; }
            set { SetPropertyValue(nameof(Manager), ref manager, value); }
        }

        /// <summary>
        /// Description of the Warehouse
        /// </summary>
        string description;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField("RuleRequiredField for Warehouse.Description",
    DefaultContexts.Save)]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue(nameof(Description), ref description, value); }
        }

        /// <summary>
        /// Platforms that are located in the Warehouse
        /// </summary>
        [Association]
    //    [RuleRequiredField("RuleRequiredField for Warehouse.Platforms",
    //DefaultContexts.Save)]
        public XPCollection<Platform> Platforms
        {
            get { return GetCollection<Platform>(nameof(Platforms)); }
        }
    }

    [NavigationItem("Port")]
    public class Platform : BaseObject
    {
        public Platform(Session session) : base(session) { }

        /// <summary>
        /// Name of the Platform
        /// </summary>
        string name;
        [RuleRequiredField("RuleRequiredField for Platform.Name",
    DefaultContexts.Save)]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue(nameof(Name), ref name, value); }
        }

        /// <summary>
        /// The cargo that is on the Platform
        /// </summary>
        float cargo;
        [RuleRequiredField("RuleRequiredField for Platform.Cargo",
    DefaultContexts.Save)]
        public float Cargo
        {
            get { return cargo; }
            set { SetPropertyValue(nameof(Cargo), ref cargo, value); }
        }

        /// <summary>
        /// The Warehouse where the Platform is located
        /// </summary>
        Warehouse warehouse;
        [Association]
        [RuleRequiredField("RuleRequiredField for Platform.Warehouse",
    DefaultContexts.Save)]
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { SetPropertyValue(nameof(Warehouse), ref warehouse, value); }
        }

        /// <summary>
        /// Pickets that are located in the Platform
        /// </summary>
        [Association]
    //    [RuleRequiredField("RuleRequiredField for Platform.Pickets",
    //DefaultContexts.Save)]
        public XPCollection<Picket> Pickets
        {
            get { return GetCollection<Picket>(nameof(Pickets)); }
        }
    }
}
