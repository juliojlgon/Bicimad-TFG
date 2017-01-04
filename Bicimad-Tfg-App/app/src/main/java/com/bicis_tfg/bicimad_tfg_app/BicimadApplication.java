package com.bicis_tfg.bicimad_tfg_app;

import android.app.Application;

import com.bicis_tfg.bicimad_tfg_app.helpers.Dagger2Helper;
import com.bicis_tfg.bicimad_tfg_app.injector.AppComponent;
import com.bicis_tfg.bicimad_tfg_app.injector.AppModule;

import net.danlew.android.joda.JodaTimeAndroid;

/**
 * Created by JulioLopez on 24/5/16.
 */
public class BicimadApplication extends Application {

    private static AppComponent mAppComponent;

    public static AppComponent getAppComponent() {
        return mAppComponent;
    }

    public static void inject(Object target) {
        Dagger2Helper.inject(AppComponent.class, mAppComponent, target);
    }

    @Override
    public void onCreate() {
        super.onCreate();
        JodaTimeAndroid.init(this);

//        // Dagger%COMPONENT_NAME%
//        mAppComponent = DaggerAppComponent.builder()
//                // list of modules that are part of this component need to be created here too
//                .appModule(new AppModule(this)) // This also corresponds to the name of your module: %component_name%Module
//                .build();

        mAppComponent = Dagger2Helper.buildComponent(AppComponent.class, new AppModule(this));
        // If a Dagger 2 component does not have any constructor arguments for any of its modules,
        // then we can use .create() as a shortcut instead:
        //  mAppComponent = com.codepath.dagger.components.DaggerNetComponent.create();
    }

}



