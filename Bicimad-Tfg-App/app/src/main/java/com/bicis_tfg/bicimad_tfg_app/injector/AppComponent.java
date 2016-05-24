package com.bicis_tfg.bicimad_tfg_app.injector;

import com.bicis_tfg.bicimad_tfg_app.LoginActivity;
import com.bicis_tfg.bicimad_tfg_app.MainActivity;

import javax.inject.Singleton;

import dagger.Component;

@Singleton
@Component(modules = {AppModule.class})
public interface AppComponent {
    void inject(MainActivity activity);
    void inject(LoginActivity loginActivity);
}
