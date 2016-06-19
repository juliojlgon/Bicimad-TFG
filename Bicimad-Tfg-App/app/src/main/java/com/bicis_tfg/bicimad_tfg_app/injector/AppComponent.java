package com.bicis_tfg.bicimad_tfg_app.injector;

import com.bicis_tfg.bicimad_tfg_app.HistoryFragment;
import com.bicis_tfg.bicimad_tfg_app.LoginActivity;
import com.bicis_tfg.bicimad_tfg_app.MainActivity;
import com.bicis_tfg.bicimad_tfg_app.MainActivityFragment;
import com.bicis_tfg.bicimad_tfg_app.MapFragment;
import com.bicis_tfg.bicimad_tfg_app.ReservationFragment;
import com.bicis_tfg.bicimad_tfg_app.renderers.ReservationRenderer;


import javax.inject.Singleton;

import dagger.Component;

@Singleton
@Component(modules = {AppModule.class})
public interface AppComponent {
    void inject(MainActivity activity);
    void inject(LoginActivity loginActivity);
    void inject(MainActivityFragment mainActivityFragment);
    void inject(MapFragment mapFragment);
    void inject(HistoryFragment historyFragment);
    void inject(ReservationFragment reservationFragment);
    void inject(ReservationRenderer reservationRenderer);
}
