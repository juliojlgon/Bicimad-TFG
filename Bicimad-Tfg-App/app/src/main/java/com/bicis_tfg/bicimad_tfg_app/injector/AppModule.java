package com.bicis_tfg.bicimad_tfg_app.injector;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.preference.PreferenceManager;

import com.bicis_tfg.bicimad_tfg_app.BicimadApplication;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;
import services.IBiciMadServices;
import services.ServiceFactory;

@Module
public class AppModule {
    private BicimadApplication mApp;

    public AppModule(BicimadApplication app) {
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

    @Provides
    BicimadApplication provideApp(){
        return mApp;
    }

    @Provides
    @Singleton
    IBiciMadServices providesServiceClient(){
        String token = PreferenceManager.getDefaultSharedPreferences(mApp).getString("Token","");
        if(token == null || token.isEmpty()){
            return ServiceFactory.createRetrofitClient();
        }else{
            return ServiceFactory.createRetrofitClient(token);
        }

    }



}
