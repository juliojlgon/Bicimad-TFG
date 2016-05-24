package com.bicis_tfg.bicimad_tfg_app;

import android.app.Application;

import com.bicis_tfg.bicimad_tfg_app.injector.AppComponent;
import com.bicis_tfg.bicimad_tfg_app.injector.AppModule;
import com.bicis_tfg.bicimad_tfg_app.injector.DaggerAppComponent;

/**
 * Created by JulioLopez on 24/5/16.
 */
public class BicimadApplication extends Application {

    private AppComponent mAppComponent;

    @Override
    public void onCreate() {
        super.onCreate();

        // Dagger%COMPONENT_NAME%
        mAppComponent = DaggerAppComponent.builder()
                // list of modules that are part of this component need to be created here too
                .appModule(new AppModule(this)) // This also corresponds to the name of your module: %component_name%Module
                .build();

        // If a Dagger 2 component does not have any constructor arguments for any of its modules,
        // then we can use .create() as a shortcut instead:
        //  mAppComponent = com.codepath.dagger.components.DaggerNetComponent.create();
    }

    public AppComponent getAppComponent() {
        return mAppComponent;
    }
}



