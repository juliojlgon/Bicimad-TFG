package com.bicis_tfg.bicimad_tfg_app;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.annimon.stream.Collectors;
import com.annimon.stream.Stream;
import com.bicis_tfg.bicimad_tfg_app.helpers.EmptyRecyclerView;
import com.bicis_tfg.bicimad_tfg_app.models.History;
import com.bicis_tfg.bicimad_tfg_app.renderers.HistoricRenderer;
import com.pedrogomez.renderers.ListAdapteeCollection;
import com.pedrogomez.renderers.RVRendererAdapter;
import com.pedrogomez.renderers.Renderer;
import com.pedrogomez.renderers.RendererBuilder;

import java.util.List;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;
import services.IBiciMadServices;

public class HistoryFragment extends Fragment {



    @BindView(R.id.recycler_view)
    EmptyRecyclerView rView;
    @BindView(R.id.empty_list_view)
    TextView emptyView;
    @BindView(R.id.swipe_layout)
    SwipeRefreshLayout swipeRefreshLayout;
    @Inject
    IBiciMadServices apiService;


    private ListAdapteeCollection<History> histories;
    private RVRendererAdapter adapter;

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;


    public HistoryFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment historyFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static HistoryFragment newInstance(String param1, String param2) {
        HistoryFragment fragment = new HistoryFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.histories = new ListAdapteeCollection<>();
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment

        View view = inflater.inflate(R.layout.fragment_history, container, false);

        ButterKnife.bind(this,view);

        getHistorical();

        swipeRefreshLayout.setOnRefreshListener(() -> getHistorical());
        RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(view.getContext());
        rView.setLayoutManager(mLayoutManager);
        rView.setItemAnimator(new DefaultItemAnimator());



        Renderer<History> renderer = new HistoricRenderer();
        RendererBuilder<History> rendererBuilder = new RendererBuilder<>(renderer);

        adapter = new RVRendererAdapter<History>(rendererBuilder, histories);
        rView.setAdapter(adapter);


        return view;
    }

    private void getHistorical() {
        swipeRefreshLayout.setRefreshing(true);
        Observable<List<History>> historyObs = apiService.getHistory();
        historyObs.subscribeOn(Schedulers.newThread()).observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                        histories -> {
                            this.histories.clear();
                            this.histories.addAll(Stream.of(histories).collect(Collectors.toList()));
                            adapter.notifyDataSetChanged();
                            rView.setEmptyView(emptyView);
                            swipeRefreshLayout.setRefreshing(false);
                        }
                );
    }


    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        BicimadApplication.inject(this);
    }

}
