//-----------------------------------------------------------------------------
// <copyright file="AccessRuleDumpItem.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    /// <summary>
    ///     AccessRuleDumpItem is a data mapping class that allows us to create
    ///     access rule items from db data in a niver fashion.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class AccessRuleDumpItem
    {
        #region Fields

        /// <summary>
        ///     param for component id
        /// </summary>
        private int componentId = 0;

        /// <summary>
        ///     param for component name
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        ///     param for access key
        /// </summary>
        private int accessKey = 0;

        /// <summary>
        ///     param for component's parent id
        /// </summary>
        private int parentId = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccessRuleDumpItem"/> class.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="name">The component name.</param>
        /// <param name="accessKey">The access key.</param>
        /// <param name="parentId">The parent id.</param>
        /// <externalUnit cref="ComponentId"/>
        /// <externalUnit cref="Name"/>
        /// <externalUnit cref="AccessKey"/>
        /// <externalUnit cref="ParentId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public AccessRuleDumpItem(
            int componentId, 
            string name, 
            int accessKey, 
            int parentId)
        {
            // set properties
            this.ComponentId = componentId;
            this.Name = name;
            this.AccessKey = accessKey;
            this.ParentId = parentId;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the component id.
        /// </summary>
        /// <value>The component id.</value>
        /// <externalUnit cref="componentId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int ComponentId
        {
            get { return this.componentId; }
            set { this.componentId = value; }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The component name.</value>
        /// <externalUnit cref="name"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        ///     Gets or sets the access key.
        /// </summary>
        /// <value>The access key.</value>
        /// <externalUnit cref="accessKey"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int AccessKey
        {
            get { return this.accessKey; }
            set { this.accessKey = value; }
        }

        /// <summary>
        ///     Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        /// <externalUnit cref="parentId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int ParentId
        {
            get { return this.parentId; }
            set { this.parentId = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns a <see cref="T:System.String"></see> that represents 
        ///     the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"></see> that represents 
        ///     the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public override string ToString()
        {
            // format the return string
            return string.Format(
                "{0}|{1}|{2}", 
                this.ComponentId, 
                this.AccessKey, 
                this.Name);
        }
        #endregion
    }
}
