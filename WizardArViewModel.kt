package app.e_skapani.ui.wizard_ar

import androidx.lifecycle.LiveData
import androidx.navigation.NavDirections
import app.e_skapani.ui.base.BaseViewModel
import app.e_skapani.ui.wizard_ar.model.WizaArUiItem
import app.e_skapani.util.livedata.SingleLiveEvent
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject


@HiltViewModel
class WizardArViewModel @Inject constructor(
) : BaseViewModel() {

    private val _navigateUi = SingleLiveEvent<NavDirections>()
    val navigateUi: LiveData<NavDirections> = _navigateUi

    private val _navigateUpUi = SingleLiveEvent<Unit>()
    val navigateUpUi: LiveData<Unit> = _navigateUpUi

    private val _wizardArUi = SingleLiveEvent<WizaArUiItem>()
    val wizardArUi: LiveData<WizaArUiItem> = _wizardArUi

    private val _showAr = SingleLiveEvent<Pair<Int, Int>>()
    val showAr: LiveData<Pair<Int, Int>> = _showAr

    var nameOfTheModel: Int = 1

    var selectedIndex: Int = 1

    fun onBackClicked() {
        _navigateUpUi.value = Unit
    }

    fun init(modelName: Int, index: Int) {
        _wizardArUi.value = WizaArUiItem.values().find { it.modelFileName == modelName }
        nameOfTheModel = modelName
        selectedIndex = index
    }

    fun onArClicked() {
        _showAr.value = Pair(nameOfTheModel, selectedIndex)
    }

}