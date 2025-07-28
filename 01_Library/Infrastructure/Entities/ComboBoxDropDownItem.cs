using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    /// <summary>
    /// Generic Class that stores the dropdown lists
    /// </summary>
    public class ComboBoxDropDownItem : Entity
    {
        private string name;

        /// <summary>
        /// String value displayed in the
        /// </summary>
        [StringLength(255)]
        public string Name
        {
            get => name;
            set => name = value;
        }

        private int? sortOrder;

        /// <summary>
        /// Order in which it appears on the dropdown list
        /// </summary>
        public int? SortOrder
        {
            get => sortOrder;
            set => sortOrder = value;
        }

        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }


}
