﻿using Firebase.Database;
using PM2E3VNRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace PM2E3VNRO.Controllers
{
    public class FirebaseController
    {
        private readonly string url = "https://pm2e3vnr-default-rtdb.firebaseio.com/";
        private readonly FirebaseClient client;
        private string deleteBugRecord;

        public FirebaseController()
        {
            this.client = new FirebaseClient(url);
        }

        public async Task<int> NewId()
        {
            List<Notas> lista = new List<Notas>();
            var dataList = await client.Child("Notas").OnceAsync<Notas>();
            foreach (var dl in dataList)
            {
                lista.Add(dl.Object);
            }

            if (lista.Count == 0)
            {
                deleteBugRecord = (await client.Child("Notas").PostAsync(new Notas())).Key;
                return 2;
            }

            return (lista.Count + 1);
        }

        //Create
        public async Task Insert(Notas nota)
        {
            string newId = (await NewId()).ToString();
            await client.Child("Notas").Child(newId).PutAsync(nota);
        }

        //Read
        public async Task<List<Notas>> SelectAll()
        {
            List<Notas> lista = new List<Notas>();
            var dataList = await client.Child("Notas").OnceAsync<Notas>();
            foreach (var dl in dataList)
            {
                if (int.TryParse(dl.Key, out int result))
                {
                    lista.Add(dl.Object);
                }
            }
            return lista;
        }

        //Read y ID
        public async Task<Notas> SelectById(int id)
        {
            return await client.Child("Notas").Child(id.ToString()).OnceSingleAsync<Notas>();
        }

        //Update
        public async void Update<Notas>(int id, Notas nota)
        {
            await client.Child("Notas").Child(id.ToString()).PutAsync(nota);
        }

        //Delete
        public async void Delete(int id)
        {
            await client.Child("Notas").Child(id.ToString()).DeleteAsync();
        }

    }
}
