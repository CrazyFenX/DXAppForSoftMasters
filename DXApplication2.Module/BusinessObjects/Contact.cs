using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;


namespace DXApplication2.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Contact : Picket
    {
        private XPCollection<AuditDataItemPersistent> changeHistory;

        public Contact(Session session) : base(session) { }

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
}
