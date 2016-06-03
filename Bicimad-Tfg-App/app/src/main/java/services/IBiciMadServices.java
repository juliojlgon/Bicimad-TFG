package services;



import com.bicis_tfg.bicimad_tfg_app.models.User;

import java.util.List;

import com.bicis_tfg.bicimad_tfg_app.models.IndexResult;
import com.bicis_tfg.bicimad_tfg_app.models.LoginResult;
import com.bicis_tfg.bicimad_tfg_app.models.Station;
import com.bicis_tfg.bicimad_tfg_app.models.ValidResult;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;
import rx.Observable;

/**
 * Created by JulioLopez on 22/5/16.
 */
public interface IBiciMadServices {

    @POST("bike/returnindex")
    Observable<IndexResult> getSummaryData();

    @POST("account/getuserdata")
    Observable<User> getUserData();

    @POST("Account/login")
    Observable<LoginResult> logUser(@Query("UserName") String username, @Query("Password") String password);

    @GET("Account/istokenvalid")
    Observable<ValidResult> isTokenValid(@Query("token") String token);

    @POST("station/fillmap")
    Observable<List<Station>> getStations();
}
