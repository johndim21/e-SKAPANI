package app.e_skapani.ui.virtual_walk.monument_details

import android.os.Bundle
import android.view.View
import androidx.lifecycle.Observer
import androidx.navigation.NavDirections
import androidx.navigation.fragment.findNavController
import androidx.navigation.navGraphViewModels
import app.e_skapani.R
import app.e_skapani.databinding.FragmentComposeBinding
import app.e_skapani.ui.base.BaseFragment
import app.e_skapani.ui.theme.EskapaniTheme
import app.e_skapani.ui.virtual_walk.VirtualWalkViewModel
import app.e_skapani.ui.virtual_walk.monument_details.composable.VirtualWalkMonumentDetailsScreen
import app.e_skapani.util.ext.safeNavigate
import app.e_skapani.util.ext.showMap
import app.e_skapani.util.ext.showModelViewerActivity
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class VirtualWalkMonumentDetailsDialogFragment : BaseFragment<FragmentComposeBinding>() {

    override fun getNavigationBarColor(): Int = R.color.nav_bar_blue_dark

    private val viewModel by navGraphViewModels<VirtualWalkViewModel>(R.id.virtual_walk_nav_graph) { defaultViewModelProviderFactory }

    override fun getViewBinding(): FragmentComposeBinding =
        FragmentComposeBinding.inflate(layoutInflater)

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        setupObservers()
        setupViews()
    }

    private fun setupViews() {
        with(binding)
        {
            composeView.setContent {
                EskapaniTheme {
                    VirtualWalkMonumentDetailsScreen(viewModel)
                }
            }
        }
    }

    private fun setupObservers() {
        with(viewModel) {
            navigateUpUi.observe(viewLifecycleOwner, Observer(::navigateUp))
            navigateUi.observe(viewLifecycleOwner, Observer(::navigate))
            showMap.observe(viewLifecycleOwner, Observer(::showMap))
            show3dViewer.observe(viewLifecycleOwner, Observer(::showModelViewerActivity))
        }
    }

    private fun navigateUp(unit: Unit) {
        findNavController().navigateUp()
    }

    private fun navigate(navDirections: NavDirections) {
        findNavController().safeNavigate(
            navDirections,
            R.id.virtualWalkMonumentDetailsDialogFragment
        )
    }

}