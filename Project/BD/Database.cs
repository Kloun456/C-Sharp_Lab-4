using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Supabase;
using Supabase.Realtime;
using Client = Supabase.Client;

namespace Project.BD
{
    public class Database : INotifyPropertyChanged
    {
        private Client Client { get; }
        public IEnumerable<TableUsers> Users { get; set; }
        public IEnumerable<TableMessages> Messages { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public Database()
        {
            Users = new List<TableUsers>();
            Messages = new List<TableMessages>();
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImVwY2xxam5mY3d6ZGlycGRya3pxIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NDQ0MzAyNjQsImV4cCI6MTk2MDAwNjI2NH0.tH3zL_jt_CNAIl_v6kZ3FF953MuvNIIGfTR5JgvS-Hg";
            var url = "https://epclqjnfcwzdirpdrkzq.supabase.co";

            Client.InitializeAsync(url, key, new SupabaseOptions
            {
                AutoConnectRealtime = true,
                ShouldInitializeRealtime = true
            });
            Client = Client.Instance;
            Client.From<TableUsers>().On(Client.ChannelEventType.All, UsersChanged);
            Client.From<TableMessages>().On(Client.ChannelEventType.All, MessagesChanged);
        }
   
        public async void LoadDataUsers()
        {
            var data = await Client.From<TableUsers>().Get();
            Users = data.Models;
            OnPropertyChangedUsers(nameof(Users));
        }

        public async void InsertDataUsers(TableUsers user)
        {
            await Client.From<TableUsers>().Insert(user);
        }

        private void UsersChanged(object sender, SocketResponseEventArgs a)
        {
            LoadDataUsers();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChangedUsers([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadDataMessages()
        {
            var data = await Client.From<TableMessages>().Get();
            Messages = data.Models;
            OnPropertyChangedUsers(nameof(Messages));
        }

        public async void InsertDataMessages(TableMessages message)
        {
            await Client.From<TableMessages>().Insert(message);
        }

        private void MessagesChanged(object sender, SocketResponseEventArgs a)
        {
            LoadDataMessages();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChangedMessages([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

