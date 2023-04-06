package app.e_skapani.ui.virtual_walk

import androidx.lifecycle.LiveData
import androidx.navigation.NavDirections
import app.e_skapani.ui.base.BaseViewModel
import app.e_skapani.ui.virtual_walk.model.VirtualWalkMonumentUiItem
import app.e_skapani.ui.virtual_walk.monument_details.VirtualWalkMonumentDetailsDialogFragmentDirections
import app.e_skapani.ui.virtual_walk.monument_selector.VirtualWalkMonumentSelectorFragmentDirections
import app.e_skapani.util.livedata.SingleLiveEvent
import com.google.android.gms.maps.model.LatLng
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class VirtualWalkViewModel @Inject constructor(
) : BaseViewModel() {

    private val _navigateUpUi = SingleLiveEvent<Unit>()
    val navigateUpUi: LiveData<Unit> = _navigateUpUi

    private val _navigateUi = SingleLiveEvent<NavDirections>()
    val navigateUi: LiveData<NavDirections> = _navigateUi

    private val _showMap = SingleLiveEvent<LatLng>()
    val showMap: LiveData<LatLng> = _showMap

    private val _show3dViewer = SingleLiveEvent<Int>()
    val show3dViewer: LiveData<Int> = _show3dViewer

    private val _selectedMonumentUi = SingleLiveEvent<VirtualWalkMonumentUiItem>()
    val selectedMonumentUi: LiveData<VirtualWalkMonumentUiItem> = _selectedMonumentUi

    lateinit var selectedMonument: VirtualWalkMonumentUiItem

    fun onBackClicked() {
        _navigateUpUi.value = Unit
    }

    fun onMonumentClicked(monument: VirtualWalkMonumentUiItem) {
        _selectedMonumentUi.value = monument
        selectedMonument = monument
    }

    fun onShowMonumentDetailsClicked() {
        _navigateUi.value =
            VirtualWalkMonumentSelectorFragmentDirections.actionVirtualWalkMonumentSelectorToVirtualWalkMonumentDetails()
    }

    fun onPhotoGalleryClicked(listIndex: Int, listOfImages: IntArray) {
        _navigateUi.value =
            VirtualWalkMonumentDetailsDialogFragmentDirections.actionVirtualWalkMonumentDetailsToVirtualWalkMonumentPhotoGallery(
                listIndex,
                listOfImages
            )
    }

    fun onMapClicked() {
        _showMap.value = selectedMonument.mapPositionLatLng
    }

    fun onArWizardClicked() {
        _navigateUi.value = VirtualWalkMonumentDetailsDialogFragmentDirections.actionVirtualWalkMonumentDetailsDialogFragmentToWizardArFragment(selectedMonument.modelFileName)
    }

    fun on3dViewerClicked() {
        _show3dViewer.value = selectedMonument.modelFileName
    }

}