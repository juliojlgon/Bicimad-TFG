package services;



import com.bicis_tfg.bicimad_tfg_app.models.History;
import com.bicis_tfg.bicimad_tfg_app.models.RegisterModel;
import com.bicis_tfg.bicimad_tfg_app.models.Reservation;
import com.bicis_tfg.bicimad_tfg_app.models.ReservationResult;
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

    @POST("Account/register")
    Observable<RegisterModel> registerUser(@Query("username") String username, @Query("email") String email, @Query("password") String password,
                                           @Query("repass") String repass);

    @GET("Account/istokenvalid")
    Observable<ValidResult> isTokenValid(@Query("token") String token);

    @POST("station/fillmap")
    Observable<List<Station>> getStations();

    @GET("station/gethistory")
    Observable<List<History>> getHistory();

    @GET("station/GetActiveReservations")
    Observable<List<Reservation>> getActiveReservations();

    @POST("station/RemoveSlotReservation")
    Observable<ReservationResult> removeSlotReservation(@Query("UserId") String userId, @Query("stationId") String stationId);

    @POST("bike/RemoveBikeReservation")
    Observable<ReservationResult> removeBikeReservation(@Query("UserId") String userId, @Query("stationId") String stationId);
}
