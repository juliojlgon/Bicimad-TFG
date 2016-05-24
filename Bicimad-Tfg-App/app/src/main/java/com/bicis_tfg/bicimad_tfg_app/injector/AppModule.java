package com.bicis_tfg.bicimad_tfg_app.injector;

import android.app.Application;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.preference.PreferenceManager;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

@Module
public class AppModule {
    private Application mApp;

    public AppModule(Application app) {
        mApp = app;
    }

    @Provides
    Resources provideResources() {
        return mApp.getResources();
    }

    @Provides
    @Singleton
    SharedPreferences provideSharedPrefs() {
        return PreferenceManager.getDefaultSharedPreferences(mApp);
    }



}
