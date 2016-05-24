package com.bicis_tfg.bicimad_tfg_app;

import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;

import butterknife.BindView;
import butterknife.ButterKnife;

public class MainActivity extends AppCompatActivity {

    @BindView(R.id.toolbar) Toolbar toolbar;
    @BindView(R.id.fab) FloatingActionButton fab;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //Inject butterknife code.
        ButterKnife.bind(this);

        setSupportActionBar(toolbar);


//        IBiciMadServices apiService = ServiceFactory.createRetrofitClient();
//
//
//        Observable<LoginResult> result = apiService.logUser("juliojlgon","123456");
//
//        result.subscribeOn(Schedulers.newThread())
//                .observeOn(AndroidSchedulers.mainThread())
//                .subscribe(r -> {
//                    Log.e("Success", r.getSuccess().toString());
//                });
//
//
//
//
//        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
//        fab.setOnClickListener(view -> {
//
//            Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
//                    .setAction("Action", null).show();
//        });
    }

}
