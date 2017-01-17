package com.bicis_tfg.bicimad_tfg_app;


import android.graphics.Color;
import android.os.Bundle;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.bicis_tfg.bicimad_tfg_app.models.RegisterModel;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;


/**
 * A simple {@link Fragment} subclass.
 * Use the {@link RegisterFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class RegisterFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";


    @BindView(R.id.register_email)
    TextView tEmail;
    @BindView(R.id.register_password)
    TextView tPass;
    @BindView(R.id.register_username)
    TextView tUsername;
    @BindView(R.id.register_repassword)
    TextView tRePass;
    @BindView(R.id.btnRegister)
    Button registerBtn;
    @BindView(R.id.btnLinkToLoginScreen)
    Button toLoginBtn;
    @BindView(R.id.register_coordinator)
    CoordinatorLayout coordinatorLayout;

    @Inject
    IBiciMadServices apiService;

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;


    public RegisterFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment RegisterFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static RegisterFragment newInstance(String param1, String param2) {
        RegisterFragment fragment = new RegisterFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_register, container, false);

        BicimadApplication.inject(this);
        ButterKnife.bind(this, view);


        registerBtn.setOnClickListener(view1 -> attempRegister());
        toLoginBtn.setOnClickListener(view1 -> {
            goToLoginFragment();
        });


        return view;
    }

    private void attempRegister() {

        if (!tPass.getText().toString().equals(tRePass.getText().toString())){
            Snackbar snackbar = Snackbar.make(coordinatorLayout, "The passwords do not match.", Snackbar.LENGTH_LONG).setAction("Action", null);
            View snackbarView = snackbar.getView();
            TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
            textView.setTextColor(Color.RED);
            snackbar.show();
            return;
        }
        Observable<RegisterModel> result = apiService.registerUser(tUsername.getText().toString(), tEmail.getText().toString(), tPass.getText().toString(),
                tRePass.getText().toString());

        result.subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(registerModel -> {
                    if (registerModel.isSuccess()) {
                        Snackbar.make(coordinatorLayout, "Congratulations. You have joined our community", Snackbar.LENGTH_LONG).setAction("Go to Login", view -> {
                            goToLoginFragment();
                        }).setActionTextColor(Color.GREEN).show();
                    }else{
                        Snackbar snackbar;
                        if(registerModel.getError() != null && !registerModel.getError().isEmpty() )
                            snackbar = Snackbar.make(coordinatorLayout, registerModel.getError(), Snackbar.LENGTH_LONG).setAction("Action", null);
                        else
                            snackbar = Snackbar.make(coordinatorLayout, "One or more fields are incorrect. Try again.", Snackbar.LENGTH_LONG).setAction("Action", null);
                        View snackbarView = snackbar.getView();
                        TextView textView = (TextView) snackbarView.findViewById(android.support.design.R.id.snackbar_text);
                        textView.setTextColor(Color.RED);
                        snackbar.show();
                    }
                }, throwable -> {
                    Snackbar.make(coordinatorLayout, "There was a problem during registration. Try again.", Snackbar.LENGTH_LONG).setAction("Action", null).show();
                });


    }

    private void goToLoginFragment() {
        LoginFragment loginFragment = new LoginFragment();
        FragmentTransaction trans = getFragmentManager().beginTransaction();
        trans.replace(R.id.fragmentcontainer, loginFragment);
        trans.addToBackStack(null);
        trans.commit();
    }

}
