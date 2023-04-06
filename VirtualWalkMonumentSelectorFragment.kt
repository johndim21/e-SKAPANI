package app.e_skapani.ui.virtual_walk.monument_selector

import android.os.Bundle
import android.view.View
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.navigation.NavDirections
import androidx.navigation.fragment.findNavController
import androidx.navigation.navGraphViewModels
import app.e_skapani.R
import app.e_skapani.databinding.FragmentComposeBinding
import app.e_skapani.ui.base.BaseFragment
import app.e_skapani.ui.dashboard.DashboardViewModel
import app.e_skapani.ui.theme.EskapaniTheme
import app.e_skapani.ui.virtual_walk.VirtualWalkViewModel
import app.e_skapani.ui.virtual_walk.monument_selector.composable.VirtualWalkMonumentSelectorScreen
import app.e_skapani.util.ext.safeNavigate
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class VirtualWalkMonumentSelectorFragment : BaseFragment<FragmentComposeBinding>() {

    override fun getNavigationBarColor(): Int = R.color.nav_bar_blue_dark

    override fun getViewBinding(): FragmentComposeBinding =
        FragmentComposeBinding.inflate(layoutInflater)

    private val viewModel by navGraphViewModels<VirtualWalkViewModel>(R.id.virtual_walk_nav_graph) { defaultViewModelProviderFactory }

    private val dashboardViewModel by activityViewModels<DashboardViewModel>()

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        setupViews()
        setupObservers()
    }

    private fun setupViews() {
        binding.composeView.setContent {
            EskapaniTheme {
                VirtualWalkMonumentSelectorScreen(viewModel, dashboardViewModel)
            }
        }
    }

    private fun setupObservers() {
        with(viewModel) {
            navigateUi.observe(viewLifecycleOwner, Observer(::navigate))
        }
    }

    private fun navigate(navDirections: NavDirections) {
        findNavController().safeNavigate(navDirections, R.id.virtualWalkMonumentSelectorFragment)
    }

}