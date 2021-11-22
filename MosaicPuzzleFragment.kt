package app.e_skapani.ui.mosaic.mosaic_puzzle

import android.annotation.SuppressLint
import android.graphics.BitmapFactory
import android.os.Bundle
import android.view.MotionEvent
import android.view.View
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.updateLayoutParams
import androidx.navigation.navGraphViewModels
import app.e_skapani.R
import app.e_skapani.base.BaseFragment
import app.e_skapani.databinding.FragmentMosaicPuzzleBinding
import app.e_skapani.ui.mosaic.MosaicViewModel
import app.e_skapani.util.ext.getStatusBarHeight
import app.e_skapani.util.helpers.dialog.showDialogForPuzzleCompilation
import timber.log.Timber
import java.util.*
import kotlin.math.sqrt

class MosaicPuzzleFragment : BaseFragment<FragmentMosaicPuzzleBinding>() {

    private val mosaicViewModel by navGraphViewModels<MosaicViewModel>(R.id.mosaic_puzzle_nav_graph)

    override fun getViewBinding(): FragmentMosaicPuzzleBinding =
        FragmentMosaicPuzzleBinding.inflate(
            layoutInflater
        )

    override fun isFullscreen(): Boolean = true

    override fun getNavigationBarColor(): Int = R.color.nav_bar_blue_dark

    override fun onDestroyView() {
        mosaicViewModel.resetTapped()
        super.onDestroyView()
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        setupValues()
        setupViews()
        setupListener()
        setupObservers()
    }

    private fun setupValues() {
        binding.timerView.start(59)
        binding.puzzleView.numberOfMoves = 0
        startPuzzleGame(mosaicViewModel.numberOfPuzzlePieces)
        mosaicViewModel.startTapped(requireActivity())
    }

    private fun setupViews() {
        binding.solutionImageView.setImageResource(mosaicViewModel.selectedImage.image)
        binding.puzzleView.setOnCompleteListener(object : PuzzleView.OnCompleteListener {
            override fun onComplete() {

                binding.solutionImageView.visibility = View.VISIBLE
                mosaicViewModel.stopTimer()

                showDialogForPuzzleCompilation(
                    requireActivity(),
                    getString(R.string.puzzle_complete_dialog_title),
                    getString(
                        R.string.puzzle_complete_dialog_message,
                        binding.puzzleView.numberOfMoves.toString()
                    ),
                    getString(R.string.puzzle_complete_dialog_try_again),
                    getString(R.string.puzzle_complete_dialog_exit)
                ) { binding.playAndResetBtn.callOnClick() }
            }
        })
    }

    @SuppressLint("ClickableViewAccessibility")
    private fun setupListener() {
        with(binding)
        {
            toolbar.backBtn.updateLayoutParams<ConstraintLayout.LayoutParams> {
                topMargin = resources.getStatusBarHeight()
            }
            toolbar.logoImg.updateLayoutParams<ConstraintLayout.LayoutParams> {
                topMargin = resources.getStatusBarHeight()
            }
            playAndResetBtn.setOnClickListener {
                timerView.stopWithOutLine()
                mosaicViewModel.resetTapped()
                timerView.start(59)
                startPuzzleGame(mosaicViewModel.numberOfPuzzlePieces)
                mosaicViewModel.startTapped(requireActivity())
            }
            seeSolutionBtn.setOnTouchListener(MyOnTouchListener())
            toolbar.backBtn.setOnClickListener {
                this@MosaicPuzzleFragment.requireActivity().onBackPressed()
            }
        }
    }

    private fun setupObservers() {
        mosaicViewModel.stopTimer.observe(viewLifecycleOwner) {
            binding.timerView.stopWitLine()
        }
        mosaicViewModel.resetTimer.observe(viewLifecycleOwner) { time ->
            binding.timerView.textstring = time
            binding.timerView.invalidate()
        }
        mosaicViewModel.updateTimer.observe(viewLifecycleOwner) { time ->
            binding.timerView.textstring = time
            binding.timerView.invalidate()
        }
    }

    private fun startPuzzleGame(pieces: Int) {
        binding.solutionImageView.visibility = View.INVISIBLE
        binding.puzzleView.visibility = View.VISIBLE
        binding.puzzleView.isPuzzleEnabled = true
        binding.puzzleView.numberOfMoves = 0
        val image =
            BitmapFactory.decodeResource(this.resources, mosaicViewModel.selectedImage.image)
        try {
            binding.puzzleView.createPuzzle(image, sqrt(pieces.toFloat()).toInt())
            binding.solutionImageView.setImageBitmap(image)
        } catch (exception: Exception) {
            Timber.e(exception)
        }
    }

    inner class MyOnTouchListener : View.OnTouchListener {
        @SuppressLint("ClickableViewAccessibility")
        override fun onTouch(v: View, event: MotionEvent): Boolean {
            when (event.action) {
                MotionEvent.ACTION_UP -> {
                    binding.solutionImageView.visibility = View.GONE
                    binding.puzzleView.isPuzzleEnabled = true
                    binding.puzzleView.visibility = View.VISIBLE
                }
                MotionEvent.ACTION_DOWN -> {
                    binding.solutionImageView.visibility = View.VISIBLE
                    binding.puzzleView.isPuzzleEnabled = false
                    binding.puzzleView.visibility = View.INVISIBLE
                }
            }
            return true
        }
    }


}