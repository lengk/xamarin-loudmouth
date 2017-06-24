using System;
using System.Collections.Generic;
using LoudMouth.Helpers;
using Realms;

namespace LoudMouth.Controllers {
    public class DataAccessController {
        Realm realm;

        public DataAccessController() {
            realm = RealmHelper.GetInstance();
        }

        public void removeAll() {
            realm.RemoveAll();
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
                savedT = realm.Add(t);
                trans.Commit();
            }
            return savedT;
        }
    }
}
