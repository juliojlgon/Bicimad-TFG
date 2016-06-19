package com.bicis_tfg.bicimad_tfg_app.injector;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.preference.PreferenceManager;

import com.bicis_tfg.bicimad_tfg_app.BicimadApplication;
import com.bicis_tfg.bicimad_tfg_app.helpers.ResourcesHelper;
import com.bicis_tfg.bicimad_tfg_app.models.CurrentUser;
import com.google.gson.Gson;

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
    ResourcesHelper provideResourcesHelper(){
        return new ResourcesHelper(mApp);
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
    IBiciMadServices providesServiceClient(){
        String token = PreferenceManager.getDefaultSharedPreferences(mApp).getString("Token","");
        if(token == null || token.isEmpty()){
            return ServiceFactory.createRetrofitClient();
        }else{
            return ServiceFactory.createRetrofitClient(token);
        }

    }
    @Provides
    CurrentUser providesCurrentUser(){
        String json = PreferenceManager.getDefaultSharedPreferences(mApp).getString("User", "");
        Gson gson = new Gson();
        CurrentUser user = gson.fromJson(json, CurrentUser.class);
        return user;
    }



}
