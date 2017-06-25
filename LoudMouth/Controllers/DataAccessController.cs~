using System;
using System.Collections.Generic;
using LoudMouth.Helpers;
using Realms;

namespace LoudMouth.Controllers {
    public class DataAccessController {
        public Realm realm;

        public DataAccessController() {
            realm = RealmHelper.GetInstance();
        }

        public void removeAll() {
            realm.Write(()=>realm.RemoveAll());
        }

        public void removeAll<T>() where T : RealmObject {
            realm.Write(() => realm.RemoveAll<T>());
        }

        public T Get<T>(string Name) where T : RealmObject {
            return realm.Find<T>(Name);
        }

        public IEnumerable<T> GetAll<T>() where T: RealmObject {
            return realm.All<T>();
        }

        public T Save<T>(T t) where T : RealmObject {
            T savedT;
            using (var trans = realm.BeginWrite()) {
                savedT = realm.Add(t, true);
                trans.Commit();
            }
            return savedT;
        }

        public void SaveAll<T>(IEnumerable<T> ts) where T: RealmObject{
            realm.Write(() => {
                foreach(T t in ts){
                    realm.Add(t, true);
                }
            });
        }
    }
}
