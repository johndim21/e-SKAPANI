package app.e_skapani.ui.wizard_ar

import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import androidx.lifecycle.Observer
import androidx.navigation.fragment.findNavController
import androidx.navigation.fragment.navArgs
import androidx.navigation.navGraphViewModels
import app.e_skapani.R
import app.e_skapani.databinding.FragmentComposeBinding
import app.e_skapani.ui.base.BaseFragment
import app.e_skapani.ui.language.model.LanguageResult
import app.e_skapani.ui.theme.EskapaniTheme
import app.e_skapani.ui.wizard_ar.composable.WizardArScreen
import app.e_skapani.util.PREFS_LANGUAGE
import app.e_skapani.util.ext.get
import app.e_skapani.util.ext.showArActivity
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class WizardArFragment() : BaseFragment<FragmentComposeBinding>() {

    private val viewModel by navGraphViewModels<WizardArViewModel>(R.id.dashboard_nav_graph) { defaultViewModelProviderFactory }

    override fun getViewBinding(): FragmentComposeBinding =
        FragmentComposeBinding.inflate(layoutInflater)

    private val args: WizardArFragmentArgs by navArgs()

    @Inject
    lateinit var preferences: SharedPreferences

    private val storedLanguage: LanguageResult by lazy {
        when (preferences[PREFS_LANGUAGE, ""] ?: "") {
            LanguageResult.GREEK.value -> LanguageResult.GREEK
            else -> LanguageResult.ENGLISH
        }
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        setupObservers()
        viewModel.init(args.modelName, args.selectedIndex)
        setupViews()
    }

    private fun setupViews() {
        with(binding)
        {
            composeView.setContent {
                EskapaniTheme {
                    WizardArScreen(viewModel)
                }
            }
        }
    }

    private fun setupObservers() {
        with(viewModel) {
            navigateUpUi.observe(viewLifecycleOwner, Observer(::navigateUp))
            showAr.observe(viewLifecycleOwner, Observer(::showAr))
        }
    }

    private fun navigateUp(unit: Unit) {
        findNavController().navigateUp()
    }

    private fun showAr(modelInfo: Pair<Int, Int>) {
        //TODO CHANGE THIS
        showArActivity(modelInfo.first, modelInfo.second.toString(), storedLanguage.value)
    }

}
