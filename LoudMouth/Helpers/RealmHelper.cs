using System;
using System.Diagnostics;
using Realms;

namespace LoudMouth.Helpers {
    public static class RealmHelper {
        public static Realm GetInstance() {
            var config = new RealmConfiguration("loudmouth.realm");
            config.ShouldDeleteIfMigrationNeeded = true;
            try {
                return Realm.GetInstance(config);
            } catch (System.Reflection.ReflectionTypeLoadException err) {
                Debug.WriteLine(err);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                Realm.DeleteRealm(config);
                return Realm.GetInstance(config);
            }
            return null;
        }
    }
}