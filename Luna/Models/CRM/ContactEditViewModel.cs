using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using Loki.Common;
using Luna.Model.CRM;

namespace Luna.UI.CRM
{
    public class ContactEditViewModel : DocumentScreen
    {
        public Contact Current { get; set; }

        public ContactEditViewModel()
        {
            DisplayName = "Nouveau contact";
        }

        #region Title

        private PropertyChangedEventArgs titleChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.Title);

        private string title;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (!object.Equals(title, value))
                {
                    title = value;
                    NotifyChangedAndDirty(titleChangedEventArgs);
                }
            }
        }

        #endregion Title

        #region Name

        private PropertyChangedEventArgs nameChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.Name);

        private string name;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (!object.Equals(name, value))
                {
                    name = value;
                    NotifyChangedAndDirty(nameChangedEventArgs);
                }
            }
        }

        #endregion Name

        #region Surname

        private PropertyChangedEventArgs surnameChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.Surname);

        private string surname;

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                if (!object.Equals(surname, value))
                {
                    surname = value;
                    NotifyChangedAndDirty(surnameChangedEventArgs);
                }
            }
        }

        #endregion Surname

        #region Phone

        private PropertyChangedEventArgs phoneChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.Phone);

        private string phone;

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                if (!object.Equals(phone, value))
                {
                    phone = value;
                    NotifyChanged(phoneChangedEventArgs);
                }
            }
        }

        #endregion Phone

        #region Mobile

        private PropertyChangedEventArgs mobileChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.Mobile);

        private string mobile;

        public string Mobile
        {
            get
            {
                return mobile;
            }

            set
            {
                if (!object.Equals(mobile, value))
                {
                    mobile = value;
                    NotifyChanged(mobileChangedEventArgs);
                }
            }
        }

        #endregion Mobile

        #region Fax

        private PropertyChangedEventArgs faxChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.Fax);

        private string fax;

        public string Fax
        {
            get
            {
                return fax;
            }

            set
            {
                if (!object.Equals(fax, value))
                {
                    fax = value;
                    NotifyChanged(faxChangedEventArgs);
                }
            }
        }

        #endregion Fax

        #region PhoneSecondary

        private PropertyChangedEventArgs phoneSecondaryChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.PhoneSecondary);

        private string phoneSecondary;

        public string PhoneSecondary
        {
            get
            {
                return phoneSecondary;
            }

            set
            {
                if (!object.Equals(phoneSecondary, value))
                {
                    phoneSecondary = value;
                    NotifyChanged(phoneSecondaryChangedEventArgs);
                }
            }
        }

        #endregion PhoneSecondary

        #region MobileSecondary

        private PropertyChangedEventArgs mobileSecondaryChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.MobileSecondary);

        private string mobileSecondary;

        public string MobileSecondary
        {
            get
            {
                return mobileSecondary;
            }

            set
            {
                if (!object.Equals(mobileSecondary, value))
                {
                    mobileSecondary = value;
                    NotifyChanged(mobileSecondaryChangedEventArgs);
                }
            }
        }

        #endregion MobileSecondary

        #region FullName

        private PropertyChangedEventArgs fullNameChangedEventArgs = ObservableHelper.CreateChangedArgs<ContactEditViewModel>(x => x.FullName);

        private string fullName;

        public string FullName
        {
            get
            {
                return fullName;
            }

            set
            {
                if (!Object.Equals(fullName, value))
                {
                    fullName = value;
                    NotifyChanged(fullNameChangedEventArgs);
                }
            }
        }

        #endregion FullName

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.WatchPropertyChanged(this, v => v.Title, v => v.UpdateName);
            this.WatchPropertyChanged(this, v => v.Surname, v => v.UpdateName);
            this.WatchPropertyChanged(this, v => v.Name, v => v.UpdateName);
            this.WatchPropertyChanged(this, v => v.FullName, v => v.UpdateDisplayName);
        }

        private void UpdateDisplayName(object sender, EventArgs e)
        {
            DisplayName = FullName;
        }

        private void UpdateName(object sender, EventArgs e)
        {
            FullName = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", Title, Surname, Name);
        }

        protected override void OnLoad()
        {
            BeginInit();
            if (Current != null)
            {
                Title = Current.Title;
                Name = Current.Name;
                Surname = Current.Surname;
                Phone = Current.Phone;
                Mobile = Current.Mobile;
                Fax = Current.Fax;
                PhoneSecondary = Current.PhoneSecondary;
                MobileSecondary = Current.MobileSecondary;
                UpdateName(this, EventArgs.Empty);
                UpdateDisplayName(this, EventArgs.Empty);
            }

            IsChanged = Current.Name == Contact.DefaultName;

            EndInit(!IsChanged);

            Task.Factory.StartNew(base.OnLoad);
        }
    }
}