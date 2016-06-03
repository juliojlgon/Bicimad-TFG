package com.bicis_tfg.bicimad_tfg_app;

import android.content.SharedPreferences;
import android.content.res.Resources;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.design.widget.Snackbar;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v4.widget.TextViewCompat;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import com.bicis_tfg.bicimad_tfg_app.models.User;
import com.google.gson.Gson;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;

public class MainActivity extends AppCompatActivity {

    @BindView(R.id.toolbar)
    Toolbar toolbar;

    @BindView(R.id.drawer_layout)
    DrawerLayout drawerLayout;

    @BindView(R.id.navigation_view)
    NavigationView navigationView;

    @Inject
    Resources resources;

    @Inject
    SharedPreferences sharedPref;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        BicimadApplication.inject(this);

        //Inject butterknife code.
        ButterKnife.bind(this);

        setSupportActionBar(toolbar);
        ActionBar actionBar = getSupportActionBar();
        actionBar.setTitle("ecoBike");

        //Set the Username in the NavigationView.
        View nView = navigationView.getHeaderView(0);
        TextView textView = (TextView) nView.findViewById(R.id.username);
        String json = sharedPref.getString(resources.getString(R.string.UserKey),"");
        Gson gson = new Gson();
        User user= gson.fromJson(json, User.class);
        textView.setText(user.getUsername());

        navigationView.setNavigationItemSelectedListener(item -> {
            int itemId = item.getItemId();
            switch (itemId) {
                case (R.id.mmoney_drawer): {
                    Snackbar.make(getCurrentFocus(), item.getTitle(), Snackbar.LENGTH_LONG)
                            .setAction("Action", null).show();
                    item.setChecked(true);
                    drawerLayout.closeDrawers();
                    return true;
                }

                case (R.id.mreservations_drawer): {
                    Snackbar.make(getCurrentFocus(), item.getTitle(), Snackbar.LENGTH_LONG)
                            .setAction("Action", null).show();
                    item.setChecked(true);
                    drawerLayout.closeDrawers();
                    return true;
                }
                case (R.id.mfavstations_drawer): {
                    Snackbar.make(getCurrentFocus(), item.getTitle(), Snackbar.LENGTH_LONG)
                            .setAction("Action", null).show();
                    item.setChecked(true);
                    drawerLayout.closeDrawers();
                    return true;
                }
                default:
                    return false;
            }
        });

        ActionBarDrawerToggle actionBarDrawerToggle = new ActionBarDrawerToggle(this,drawerLayout,toolbar,R.string.openDrawer, R.string.closeDrawer) {

            @Override
            public void onDrawerClosed(View drawerView) {
                // Code here will be triggered once the drawer closes as we dont want anything to happen so we leave this blank
                super.onDrawerClosed(drawerView);
            }

            @Override
            public void onDrawerOpened(View drawerView) {
                // Code here will be triggered once the drawer open as we dont want anything to happen so we leave this blank
                super.onDrawerOpened(drawerView);
            }
        };
        drawerLayout.addDrawerListener(actionBarDrawerToggle);
        actionBarDrawerToggle.syncState();



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

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }


}
