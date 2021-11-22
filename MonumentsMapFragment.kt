package app.e_skapani.ui.monument.monument_map

import android.annotation.SuppressLint
import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import android.view.ViewTreeObserver.OnGlobalLayoutListener
import android.widget.ImageView
import android.widget.RelativeLayout
import androidx.activity.OnBackPressedCallback
import androidx.constraintlayout.motion.widget.MotionLayout
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.updateLayoutParams
import androidx.fragment.app.activityViewModels
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import app.e_skapani.R
import app.e_skapani.base.BaseFragment
import app.e_skapani.databinding.FragmentMonumentsMapBinding
import app.e_skapani.ui.dashboard.DashboardViewModel
import app.e_skapani.ui.helpers.ImageCarouselItemAdapter
import app.e_skapani.ui.monument.MonumentViewModel
import app.e_skapani.util.enums.MonumentType
import app.e_skapani.util.ext.*
import com.google.android.material.tabs.TabLayoutMediator
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class MonumentsMapFragment : BaseFragment<FragmentMonumentsMapBinding>() {

    @Inject
    lateinit var prefs: SharedPreferences

    private val monumentViewModel by viewModels<MonumentViewModel>()

    private val dashboardViewModel by activityViewModels<DashboardViewModel>()

    override fun isFullscreen(): Boolean = true

    override fun getNavigationBarColor(): Int = R.color.nav_bar_blue

    override fun getViewBinding(): FragmentMonumentsMapBinding =
        FragmentMonumentsMapBinding.inflate(layoutInflater)

    private val imageCarouselItemAdapter: ImageCarouselItemAdapter by lazy {
        ImageCarouselItemAdapter(this::onImageClickListener)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        setupViews()
        setupListeners()
        setupObservers()
    }

    private fun setupObservers() {
        monumentViewModel.updateMonumentSelected.observe(viewLifecycleOwner) { selectedMonument ->
            //hide Select monument text
            binding.monumentSelectorContainer.chooseAMonumentTxt.visibility = View.INVISIBLE
            //show go to monument details btn
            binding.goToDetailsBtn.visibility = View.VISIBLE
            //Update monument selection row
            updateMonumentSelector(selectedMonument)
        }
    }

    private fun setupViews() {
        setupBubbleIcons()
        setupSpaceForWalkIcon()
        setupMonumentSelectionRow()
    }

    private fun setupSpaceForWalkIcon() {
        binding.monumentSelectorContainer.root.viewTreeObserver.addOnGlobalLayoutListener(object :
            OnGlobalLayoutListener {
            override fun onGlobalLayout() {
                binding.monumentSelectorContainer.root.viewTreeObserver.removeOnGlobalLayoutListener(
                    this
                )
                binding.monumentSelectorContainer.root.setPadding(
                    0,
                    binding.walkingIcon.height / 2,
                    0,
                    0
                )
            }
        })
    }

    private fun setupListeners() {
        with(binding)
        {
            fullContainer.viewTreeObserver.addOnGlobalLayoutListener {
                val transitionListener = object : MotionLayout.TransitionListener {

                    override fun onTransitionStarted(p0: MotionLayout?, startId: Int, endId: Int) {
                    }

                    @SuppressLint("FragmentBackPressedCallback")
                    override fun onTransitionChange(
                        p0: MotionLayout?,
                        startId: Int,
                        endId: Int,
                        progress: Float
                    ) {
                        if (startId == R.id.show_monument_details) {
                            monumentBottomNavigationContainer.root.visibility = View.GONE
                            toolbar.menuBtn.setImageResource(R.drawable.ic_burger)
                            toolbar.menuBtn.setOnClickListener {
                                dashboardViewModel.openMenu()
                            }
                            activity?.onBackPressedDispatcher?.addCallback(
                                this@MonumentsMapFragment,
                                object : OnBackPressedCallback(true) {
                                    @SuppressLint("FragmentBackPressedCallback")
                                    override fun handleOnBackPressed() {
                                        this@MonumentsMapFragment.findNavController().navigateUp()
                                    }
                                })
                        }
                    }

                    @SuppressLint("FragmentBackPressedCallback")
                    override fun onTransitionCompleted(p0: MotionLayout?, currentId: Int) {
                        if (currentId == R.id.show_monument_details) {
                            monumentBottomNavigationContainer.root.visibility = View.VISIBLE
                            toolbar.menuBtn.setImageResource(R.drawable.ic_back)
                            toolbar.menuBtn.setOnClickListener {
                                fullContainer.setTransition(R.id.start_transition)
                                fullContainer.transitionToEnd()
                            }
                            activity?.onBackPressedDispatcher?.addCallback(
                                this@MonumentsMapFragment,
                                object : OnBackPressedCallback(true) {
                                    @SuppressLint("FragmentBackPressedCallback")
                                    override fun handleOnBackPressed() {
                                        fullContainer.setTransition(R.id.start_transition)
                                        fullContainer.transitionToEnd()
                                    }
                                })
                        }

                    }

                    override fun onTransitionTrigger(
                        p0: MotionLayout?,
                        triggerId: Int,
                        positive: Boolean,
                        progress: Float
                    ) {

                    }
                }

                fullContainer.setTransitionListener(transitionListener)
            }

            toolbar.menuBtn.updateLayoutParams<ConstraintLayout.LayoutParams> {
                topMargin = resources.getStatusBarHeight()
            }
            toolbar.logoImg.updateLayoutParams<ConstraintLayout.LayoutParams> {
                topMargin = resources.getStatusBarHeight()
            }
            toolbar.menuBtn.setOnClickListener {
                dashboardViewModel.openMenu()
            }
            monumentBottomNavigationContainer.mapImgBtn.setOnClickListener {
                this@MonumentsMapFragment.requireActivity().actionToGoogleMaps(
                    monumentViewModel.selectedMonument!!.mapPositionLatLng.latitude,
                    monumentViewModel.selectedMonument!!.mapPositionLatLng.longitude
                )
            }
            monumentBottomNavigationContainer.arImgBtn.setOnClickListener {
                showArActivity()
            }
            monumentBottomNavigationContainer.modelViewImgBtn.setOnClickListener {
                showModelViewerActivity(monumentViewModel.selectedMonument?.modelFileName ?: "")
            }

            fullContainer.setTransitionListener(object : MotionLayout.TransitionListener {
                override fun onTransitionStarted(motionLayout: MotionLayout, i: Int, i1: Int) {

                }

                override fun onTransitionChange(
                    motionLayout: MotionLayout,
                    i: Int,
                    i1: Int,
                    v: Float
                ) {
                }

                override fun onTransitionCompleted(motionLayout: MotionLayout, i: Int) {

                }

                override fun onTransitionTrigger(
                    motionLayout: MotionLayout,
                    i: Int,
                    b: Boolean,
                    v: Float
                ) {

                }
            })
        }
    }

    private fun setupDotIndicator() {
        val tabLayoutMediator = TabLayoutMediator(
            binding.imageIndicator, binding.imageCarousel, true
        ) { tab, position -> }
        tabLayoutMediator.attach()
    }

    private fun setupAdapter() {
        binding.imageCarousel.adapter = imageCarouselItemAdapter
        imageCarouselItemAdapter.submitList(monumentViewModel.selectedMonument?.listOfImages?.toMutableList())
    }

    private fun setupBubbleIcons() {
        binding.fullContainer.viewTreeObserver.addOnGlobalLayoutListener(object :
            OnGlobalLayoutListener {
            override fun onGlobalLayout() {
                binding.fullContainer.viewTreeObserver.removeOnGlobalLayoutListener(this)
                val mHeightPixels = binding.fullContainer.height
                val mWidthPixels = binding.fullContainer.width
                MonumentType.values().forEach { monument ->
                    val monumentBubbleImage = ImageView(this@MonumentsMapFragment.requireContext())
                    monumentBubbleImage.setImageResource(monument.bubbleIcon)
                    monumentBubbleImage.scaleType = ImageView.ScaleType.FIT_CENTER
                    monumentBubbleImage.setOnClickListener {
                        monumentViewModel.monumentOnCLickListen(monument)
                    }
                    val params = RelativeLayout.LayoutParams(mWidthPixels / 10, mHeightPixels / 10)
                    params.leftMargin = (mWidthPixels * monument.bubblePositionWidth).toInt()
                    params.topMargin = (mHeightPixels * monument.bubblePositionHeight).toInt()
                    binding.bubbleImageLayout.addView(monumentBubbleImage, params)
                }
            }
        })
    }

    private fun setupMonumentSelectionRow() {
        binding.monumentSelectorContainer.monumentSelectorImages.rotontaMonument.setOnClickListener {
            monumentViewModel.monumentOnCLickListen(MonumentType.ROTONTA)
        }
        binding.monumentSelectorContainer.monumentSelectorImages.kamaraMonument.setOnClickListener {
            monumentViewModel.monumentOnCLickListen(MonumentType.KAMARA)
        }
        binding.monumentSelectorContainer.monumentSelectorImages.ipodromosMonument.setOnClickListener {
            monumentViewModel.monumentOnCLickListen(MonumentType.IPONDROMOS)
        }
        binding.monumentSelectorContainer.monumentSelectorImages.oktagonoMonument.setOnClickListener {
            monumentViewModel.monumentOnCLickListen(MonumentType.OKTAGONO)
        }
        binding.monumentSelectorContainer.monumentSelectorImages.vasilikiMonument.setOnClickListener {
            monumentViewModel.monumentOnCLickListen(MonumentType.VASILIKI)
        }
    }

    private fun updateMonumentSelector(selectedMonument: MonumentType) {
        MonumentType.values().forEach { monument ->
            when (monument) {
                MonumentType.ROTONTA -> {
                    binding.monumentSelectorContainer.monumentSelectorImages.rotontaMonument.isSelected =
                        monument == selectedMonument
                    if (monument == selectedMonument) {
                        binding.monumentSelectorContainer.monumentSelectorImages.rotontaMonumentText.visibility =
                            View.VISIBLE
                    } else {
                        binding.monumentSelectorContainer.monumentSelectorImages.rotontaMonumentText.visibility =
                            View.INVISIBLE
                    }
                }
                MonumentType.KAMARA -> {
                    binding.monumentSelectorContainer.monumentSelectorImages.kamaraMonument.isSelected =
                        monument == selectedMonument
                    if (monument == selectedMonument) {
                        binding.monumentSelectorContainer.monumentSelectorImages.kamaraMonumentText.visibility =
                            View.VISIBLE
                    } else {
                        binding.monumentSelectorContainer.monumentSelectorImages.kamaraMonumentText.visibility =
                            View.INVISIBLE
                    }
                }
                MonumentType.IPONDROMOS -> {
                    binding.monumentSelectorContainer.monumentSelectorImages.ipodromosMonument.isSelected =
                        monument == selectedMonument
                    if (monument == selectedMonument) {
                        binding.monumentSelectorContainer.monumentSelectorImages.ipodromosMonumentText.visibility =
                            View.VISIBLE
                    } else {
                        binding.monumentSelectorContainer.monumentSelectorImages.ipodromosMonumentText.visibility =
                            View.INVISIBLE
                    }
                }
                MonumentType.OKTAGONO -> {
                    binding.monumentSelectorContainer.monumentSelectorImages.oktagonoMonument.isSelected =
                        monument == selectedMonument
                    if (monument == selectedMonument) {
                        binding.monumentSelectorContainer.monumentSelectorImages.oktagonoMonumentText.visibility =
                            View.VISIBLE
                    } else {
                        binding.monumentSelectorContainer.monumentSelectorImages.oktagonoMonumentText.visibility =
                            View.INVISIBLE
                    }
                }
                MonumentType.VASILIKI -> {
                    binding.monumentSelectorContainer.monumentSelectorImages.vasilikiMonument.isSelected =
                        monument == selectedMonument
                    if (monument == selectedMonument) {
                        binding.monumentSelectorContainer.monumentSelectorImages.vasilikiMonumentText.visibility =
                            View.VISIBLE
                    } else {
                        binding.monumentSelectorContainer.monumentSelectorImages.vasilikiMonumentText.visibility =
                            View.INVISIBLE
                    }
                }
            }
            //Show the selected monument small description
            binding.monumentSelectorContainer.monumentSmallDescriptionLayout.root.visibility =
                View.VISIBLE
            binding.monumentSelectorContainer.monumentSmallDescriptionLayout.monumentSmallDescriptionTxt.text =
                resources.getString(selectedMonument.smallDescription)
            binding.bigDescriptionText.text =
                getString(monumentViewModel.selectedMonument?.fullDescription ?: 0)
            setupAdapter()
            setupDotIndicator()
        }
    }

    private fun onImageClickListener(images: MutableList<Int>, carouselPosition: Int) {
        showFullCarouselImagesActivity(images, carouselPosition)
    }

}