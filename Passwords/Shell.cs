using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Passwords
{
    public class Shell
    {
        private static Dictionary<string, Page> _pages = new Dictionary<string, Page>();

        public static Page CurrentPage
        {
            get
            {
                return _currentPage; 
            }

            set
            {
                _currentPage = value;
                _pageChanged();
            }
        }
        private static Page _currentPage = null;

        public delegate void PageChangeEventHandler();
        private static event PageChangeEventHandler _pageChanged;

        public void RegisterPage(string nameOfPage, Page page)
        {
            _pages.Add(nameOfPage, page);
        }

        public void SubscribeOnEvent(PageChangeEventHandler onPageChange)
        {
            _pageChanged += onPageChange;
        }

        public static void GoTo(string pageName)
        {
            if (pageName == "..")
            {
                //Go to previos page in stack. Make using stack 
            }
            else
            {
                ChangePage(_pages[pageName]);
            }            
        }

        private static void ChangePage(Page destinantionPage)
        {
            CurrentPage = destinantionPage;
        }
    }
}
