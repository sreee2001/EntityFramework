using System;

namespace Infrastructure.Entities
{
    /// <summary>
    /// Base Class for all entities to be saved to database
    /// </summary>
    public abstract class Entity
    {
        private int _id;

        /// <inheritdoc />
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        #region Implementation of IAuditInfo

        private string _createdBy;

        /// <inheritdoc />
        public string CreatedBy
        {
            get => _createdBy;
            set => _createdBy = value;
        }

        private string _modifiedBy;

        /// <inheritdoc />
        public string ModifiedBy
        {
            get => _modifiedBy;
            set => _modifiedBy = value;
        }

        private DateTime? _createdDate;

        /// <inheritdoc />
        public DateTime? CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value;
        }

        private DateTime? _modifiedDate;

        /// <inheritdoc />
        public DateTime? ModifiedDate
        {
            get => _modifiedDate;
            set => _modifiedDate = value;
        }

        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        protected Entity()
        {
            SetAuditInfo();
        }

        /// <summary>
        /// Updates the audit Information
        /// </summary>
        public void SetAuditInfo()
        {
            if (Id == 0)
            {
                CreatedBy = Environment.UserName;
                CreatedDate = DateTime.Now;
            }
            ModifiedBy = Environment.UserName;
            ModifiedDate = DateTime.Now;
        }
    }


}
