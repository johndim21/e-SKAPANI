//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by Fernflower decompiler)
//

package app.e_skapani.ui.mosaic.mosaic_puzzle

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.graphics.Color
import android.util.AttributeSet
import android.view.View
import android.widget.ImageView
import android.widget.ImageView.ScaleType
import android.widget.RelativeLayout
import androidx.annotation.RequiresApi
import androidx.core.view.ViewCompat
import com.rp.puzzle.R.styleable
import com.rp.puzzle.bean.Block
import com.rp.puzzle.util.BitmapUtil
import java.util.*

class PuzzleView : RelativeLayout {
    private var puzzleViewHeight = 0
    private var puzzleViewWidth = 0
    private var blockViewHeight = 0
    private var blockViewWidth = 0
    var puzzleSize = 0
        private set
    private var mBitmap: Bitmap? = null
    private var puzzleBlockState: Array<Array<Block?>>? = null
    private var isParentInflated = false
    private var isDataRecieved = false
    private var mOnCompleteListener: OnCompleteListener? = null
    private var mode = 0
    private var puzzleBorderSize = 0
    private var puzzleBorderColor = -1
    var isPuzzleEnabled = true
    var numberOfMoves: Int = 0

    lateinit var whiteBlock: Block

    constructor(context: Context?) : super(context) {
        getCustomAttributes(null as AttributeSet?)
        init()
    }

    constructor(context: Context?, attrs: AttributeSet?) : super(context, attrs) {
        getCustomAttributes(attrs)
        init()
    }

    constructor(context: Context?, attrs: AttributeSet?, defStyleAttr: Int) : super(
        context,
        attrs,
        defStyleAttr
    ) {
        getCustomAttributes(attrs)
        init()
    }

    @RequiresApi(api = 21)
    constructor(
        context: Context?,
        attrs: AttributeSet?,
        defStyleAttr: Int,
        defStyleRes: Int
    ) : super(context, attrs, defStyleAttr, defStyleRes) {
        getCustomAttributes(attrs)
        init()
    }

    private fun getCustomAttributes(attrs: AttributeSet?) {
        if (attrs != null) {
            val mTypedArray =
                this.context.theme.obtainStyledAttributes(attrs, styleable.PuzzleView, 0, 0)
            try {
                puzzleBorderSize = mTypedArray.getInt(styleable.PuzzleView_pv_borderSize, 0)
                puzzleBorderColor = mTypedArray.getColor(styleable.PuzzleView_pv_borderColor, -1)
                isPuzzleEnabled = mTypedArray.getBoolean(styleable.PuzzleView_pv_enabled, true)
            } finally {
                mTypedArray.recycle()
            }
        }
    }

    private fun init() {
        this.viewTreeObserver.addOnGlobalLayoutListener {
            if (!isParentInflated) {
                puzzleViewWidth = this@PuzzleView.width
                puzzleViewHeight = this@PuzzleView.height
                isParentInflated = true
                if (isDataRecieved) {
                    if (mode == 1) {
                        puzzleBlockState = randomBlock
                    }
                    showPuzzle()
                }
            }
        }
    }

    @Throws(Exception::class)
    fun createPuzzle(resId: Int, puzzleSize: Int) {
        mode = 1
        mBitmap = BitmapFactory.decodeResource(this.resources, resId)
        this.puzzleSize = puzzleSize
        if (mBitmap == null) {
            throw Exception("Invalid resource Id!")
        } else if (puzzleSize < 2) {
            throw Exception("puzzle size should be greater than 1")
        } else {
            puzzleBlockState = randomBlock
            isDataRecieved = true
            if (isParentInflated) {
                showPuzzle()
            }
        }
    }

    @Throws(Exception::class)
    fun createPuzzle(mBitmap: Bitmap?, puzzleSize: Int) {
        mode = 1
        this.mBitmap = mBitmap
        this.puzzleSize = puzzleSize
        puzzleBlockState = randomBlock
        if (mBitmap == null) {
            throw Exception("Bitmap can't be null!")
        } else if (puzzleSize < 2) {
            throw Exception("puzzle size should be greater than 1")
        } else {
            puzzleBlockState = randomBlock
            isDataRecieved = true
            if (isParentInflated) {
                showPuzzle()
            }
        }
    }

    @Throws(Exception::class)
    fun playPuzzle(mBitmap: Bitmap?, mBlocks: Array<Array<Block?>>?) {
        mode = 2
        this.mBitmap = mBitmap
        puzzleBlockState = mBlocks
        if (mBitmap == null) {
            throw Exception("Bitmap can't be null!")
        } else if (mBlocks == null) {
            throw Exception("Bloc[][] can't be null!")
        } else {
            val nosCols = mBlocks.size
            val nosRows: Int = mBlocks[0].size
            if (nosCols == nosRows) {
                puzzleSize = nosCols
                if (puzzleSize < 2) {
                    throw Exception("Bloc[][] should be n*m where n==m & n>1!")
                } else {
                    isDataRecieved = true
                    if (isParentInflated) {
                        showPuzzle()
                    }
                }
            } else {
                throw Exception("Bloc[][] should be n*m where n==m!")
            }
        }
    }

    @Throws(Exception::class)
    fun playPuzzle(resId: Int, mBlocks: Array<Array<Block?>>?) {
        mode = 2
        mBitmap = BitmapFactory.decodeResource(this.resources, resId)
        puzzleBlockState = mBlocks
        if (mBitmap == null) {
            throw Exception("Invalid resource Id!")
        } else if (mBlocks == null) {
            throw Exception("Bloc[][] can't be null!")
        } else {
            val nosCols = mBlocks.size
            val nosRows: Int = mBlocks[0].size
            if (nosCols == nosRows) {
                puzzleSize = nosCols
                if (puzzleSize < 2) {
                    throw Exception("Bloc[][] should be n*m where n==m & n>1!")
                } else {
                    isDataRecieved = true
                    if (isParentInflated) {
                        showPuzzle()
                    }
                }
            } else {
                throw Exception("Bloc[][] should be n*m where n==m & n>2!")
            }
        }
    }

    private fun showPuzzle() {
        removeAllViews()
        blockViewHeight = puzzleViewHeight / puzzleSize
        blockViewWidth = puzzleViewWidth / puzzleSize
        if (blockViewHeight != 0 && blockViewWidth != 0) {
            mBitmap = BitmapUtil.scaleBitmap(mBitmap, puzzleViewWidth, puzzleViewHeight)
            for (i in 0 until puzzleSize) {
                for (j in 0 until puzzleSize) {
                    showBlock(puzzleBlockState!![i][j])
                }
            }
        }
    }

    @SuppressLint("ClickableViewAccessibility")
    private fun setTouchEvents(mView: View, mBlock: Block) {
        mView.setOnTouchListener { view, event ->
            if (!isPuzzleEnabled) {
                true
            } else {
                resetZIndex()
                ViewCompat.setZ(mView, 1.0f)
                val maxLeftMargin = puzzleViewWidth - blockViewWidth
                val maxTopMargin = puzzleViewHeight - blockViewHeight
                val X = event.rawX.toInt()
                val Y = event.rawY.toInt()
                val mLayoutParams = view.layoutParams as LayoutParams
                var blockMaxTopMargn: Int
                when (event.action and 255) {
                    0 -> {
                        mBlock._xDelta = X - mLayoutParams.leftMargin
                        mBlock._yDelta = Y - mLayoutParams.topMargin
                    }
                    1 -> {
                        val currentLeftMargin = mLayoutParams.leftMargin
                        val currentTopMargin = mLayoutParams.topMargin
                        var blockPositionX = 0
                        var blockPositionY = 0
                        while (blockPositionX < puzzleSize) {
                            blockMaxTopMargn = blockViewWidth * blockPositionX + blockViewWidth / 2
                            if (blockPositionX == 0) {
                                if (currentLeftMargin < blockMaxTopMargn) {
                                    break
                                }
                                ++blockPositionX
                            } else {
                                if (currentLeftMargin <= blockMaxTopMargn) {
                                    break
                                }
                                ++blockPositionX
                            }
                        }
                        while (blockPositionY < puzzleSize) {
                            blockMaxTopMargn =
                                blockViewHeight * blockPositionY + blockViewHeight / 2
                            if (blockPositionY == 0) {
                                if (currentTopMargin < blockMaxTopMargn) {
                                    break
                                }
                                ++blockPositionY
                            } else {
                                if (currentTopMargin <= blockMaxTopMargn) {
                                    break
                                }
                                ++blockPositionY
                            }
                        }
                        swapBlock(
                            mBlock,
                            puzzleBlockState?.get(blockPositionX)?.get(blockPositionY)
                        )
                    }
                    2 -> {
                        blockMaxTopMargn = X - mBlock._xDelta
                        val topMargin = Y - mBlock._yDelta
                        if (blockMaxTopMargn >= 0 && blockMaxTopMargn <= maxLeftMargin) {
                            mLayoutParams.leftMargin = X - mBlock._xDelta
                        }
                        if (topMargin >= 0 && topMargin <= maxTopMargin) {
                            mLayoutParams.topMargin = Y - mBlock._yDelta
                        }
                        view.layoutParams = mLayoutParams
                    }
                    3, 4, 5, 6 -> {
                    }
                }
                true
            }
        }
    }

    private fun showBlock(mBlock: Block?) {
        val mImageView = ImageView(this.context)
        if (mBlock!!.realX == puzzleSize - 1 && mBlock.realY == puzzleSize - 1) {
            val bitmap =
                Bitmap.createBitmap(blockViewWidth, blockViewHeight, Bitmap.Config.ARGB_8888)
            bitmap.eraseColor(Color.WHITE)
            mImageView.setImageBitmap(bitmap)
        } else {
            mImageView.setImageBitmap(
                BitmapUtil.cropBitmap(
                    mBitmap, mBlock.getRealLeftMargin(
                        puzzleSize, puzzleViewWidth
                    ), mBlock.getRealTopMargin(puzzleSize, puzzleViewHeight),
                    blockViewWidth, blockViewHeight, puzzleBorderSize, puzzleBorderColor
                )
            )
            mImageView.scaleType = ScaleType.FIT_XY
        }
        mBlock.setmImageView(mImageView)
        setOnClickEvent(mImageView, mBlock)
        val params = LayoutParams(blockViewWidth, blockViewHeight)
        params.addRule(9, -1)
        params.leftMargin = mBlock.getCurrentLeftMargin(puzzleSize, puzzleViewWidth)
        params.topMargin = mBlock.getCurrentTopMargin(puzzleSize, puzzleViewHeight)
        this.addView(mImageView, params)
    }


    @SuppressLint("ClickableViewAccessibility")
    private fun setOnClickEvent(mView: View, mBlock: Block) {
        mView.setOnClickListener { view ->
            if (isPuzzleEnabled) {
                var bx = mBlock!!.currentX
                var by = mBlock!!.currentY
                var wx = whiteBlock.currentX
                var wy = whiteBlock.currentY
                if (mBlock != whiteBlock &&
                    (wx == bx && wy == by - 1) ||
                    (wx == bx && wy == by + 1) ||
                    (wx - 1 == bx && wy == by) ||
                    (wx + 1 == bx && wy == by)
                ) {
                    swapBlock(mBlock, whiteBlock)
                    numberOfMoves++
                }
            }
        }
//        mView.setOnTouchListener { view, event ->
//            if (!isPuzzleEnabled) {
//                true
//            } else {
//                resetZIndex()
//                ViewCompat.setZ(mView, 1.0f)
//                val maxLeftMargin = puzzleViewWidth - blockViewWidth
//                val maxTopMargin = puzzleViewHeight - blockViewHeight
//                val X = event.rawX.toInt()
//                val Y = event.rawY.toInt()
//                val mLayoutParams = view.layoutParams as LayoutParams
//                var blockMaxTopMargn: Int
//                when (event.action and 255) {
//                    0 -> {
//                        mBlock._xDelta = X - mLayoutParams.leftMargin
//                        mBlock._yDelta = Y - mLayoutParams.topMargin
//                    }
//                    1 -> {
//                        val currentLeftMargin = mLayoutParams.leftMargin
//                        val currentTopMargin = mLayoutParams.topMargin
//                        var blockPositionX = 0
//                        var blockPositionY = 0
//                        while (blockPositionX < puzzleSize) {
//                            blockMaxTopMargn = blockViewWidth * blockPositionX + blockViewWidth / 2
//                            if (blockPositionX == 0) {
//                                if (currentLeftMargin < blockMaxTopMargn) {
//                                    break
//                                }
//                                ++blockPositionX
//                            } else {
//                                if (currentLeftMargin <= blockMaxTopMargn) {
//                                    break
//                                }
//                                ++blockPositionX
//                            }
//                        }
//                        while (blockPositionY < puzzleSize) {
//                            blockMaxTopMargn = blockViewHeight * blockPositionY + blockViewHeight / 2
//                            if (blockPositionY == 0) {
//                                if (currentTopMargin < blockMaxTopMargn) {
//                                    break
//                                }
//                                ++blockPositionY
//                            } else {
//                                if (currentTopMargin <= blockMaxTopMargn) {
//                                    break
//                                }
//                                ++blockPositionY
//                            }
//                        }
//
//
//                        var bx = mBlock!!.currentX
//                        var by = mBlock!!.currentY
//                        var wx = whiteBlock.currentX
//                        var wy = whiteBlock.currentY
//                        if(mBlock!=whiteBlock &&
//                            (wx==bx && wy==by-1) ||
//                            (wx==bx && wy==by+1) ||
//                            (wx-1==bx && wy==by) ||
//                            (wx+1==bx && wy==by)
//                                )
//                        {
////                            if (event.)
//                            swapBlock(mBlock, whiteBlock)
//                            numberOfMoves++
//                        }
//                    }
//                    2,3, 4, 5, 6 -> {
//                    }
//                }
//                true
//            }
//        }
    }


    private fun repositionBlock(mBlock: Block?) {
        val layoutParams1 = mBlock!!.getmImageView().layoutParams as LayoutParams
        layoutParams1.leftMargin = mBlock.getCurrentLeftMargin(puzzleSize, puzzleViewWidth)
        layoutParams1.topMargin = mBlock.getCurrentTopMargin(puzzleSize, puzzleViewHeight)
        mBlock.getmImageView().layoutParams = layoutParams1
    }

    private fun swapBlock(mBlock1: Block?, mBlock2: Block?) {
        val temBlock = mBlock1!!.copy
        mBlock1.changePosition(mBlock2)
        mBlock2!!.changePosition(temBlock)
        repositionBlock(mBlock1)
        repositionBlock(mBlock2)
        puzzleBlockState!![mBlock1.currentX][mBlock1.currentY] = mBlock1
        puzzleBlockState!![mBlock2.currentX][mBlock2.currentY] = mBlock2
        if (isPuzzleCompleted && mOnCompleteListener != null) {
            mOnCompleteListener!!.onComplete()
        }
    }

    private val isPuzzleCompleted: Boolean
        private get() {
            for (i in 0 until puzzleSize) {
                for (j in 0 until puzzleSize) {
                    val mBlock = puzzleBlockState!![i][j]
                    if (mBlock!!.realX != i || mBlock.realY != j) {
                        return false
                    }
                }
            }
            return true
        }
    private val randomBlock: Array<Array<Block?>>
        private get() {
            val mBlocksArray = Array(puzzleSize) {
                arrayOfNulls<Block>(
                    puzzleSize
                )
            }
            var i: Int
            var j: Int
            i = 0
            while (i < puzzleSize) {
                j = 0
                while (j < puzzleSize) {
                    mBlocksArray[i][j] = Block(i, j, i, j)
                    ++j
                }
                ++i
            }
            i = 0
            whiteBlock = mBlocksArray[puzzleSize - 1][puzzleSize - 1]!!
            while (i < puzzleSize) {
                j = 0
                while (j < puzzleSize) {
                    val rn = Random()
                    val randomI = 0 + rn.nextInt(puzzleSize)
                    val randomJ = 0 + rn.nextInt(puzzleSize)
                    val b1 = mBlocksArray[i][j]
                    val b2 = mBlocksArray[randomI][randomJ]
                    val temBlock = b1!!.copy
                    b1.changePosition(b2)
                    b2!!.changePosition(temBlock)
                    mBlocksArray[randomI][randomJ] = b1
                    mBlocksArray[i][j] = b2
                    ++j
                }
                ++i
            }
            var random = mBlocksArray[puzzleSize - 1][puzzleSize - 1]
            var oldWhiteX = whiteBlock!!.currentX
            var oldWhiteY = whiteBlock!!.currentY
            val temBlock = whiteBlock!!.copy
            whiteBlock.changePosition(random)
            random!!.changePosition(temBlock)
            mBlocksArray[puzzleSize - 1][puzzleSize - 1] = whiteBlock
            mBlocksArray[oldWhiteX][oldWhiteY] = random
            return mBlocksArray
        }

    fun autoSolve() {
        if (isDataRecieved && isParentInflated) {
            val tem = Array(puzzleSize) {
                arrayOfNulls<Block>(
                    puzzleSize
                )
            }
            for (i in 0 until puzzleSize) {
                for (j in 0 until puzzleSize) {
                    val mBlocktem = puzzleBlockState!![i][j]
                    mBlocktem!!.currentX = mBlocktem.realX
                    mBlocktem.currentY = mBlocktem.realY
                    tem[mBlocktem.realX][mBlocktem.realY] = mBlocktem
                }
            }
            puzzleBlockState = tem
            showPuzzle()
        }
    }

    fun setOnCompleteListener(mOnCompleteListener: OnCompleteListener?) {
        this.mOnCompleteListener = mOnCompleteListener
    }

    fun getmOnCompleteListener(): OnCompleteListener? {
        return mOnCompleteListener
    }

    fun getPuzzleBorderSize(): Int {
        return puzzleBorderSize
    }

    fun setPuzzleBorderSize(puzzleBorderSize: Int) {
        this.puzzleBorderSize = puzzleBorderSize
        showPuzzle()
    }

    fun getPuzzleBorderColor(): Int {
        return puzzleBorderColor
    }

    fun setPuzzleBorderColor(puzzleBorderColor: Int) {
        this.puzzleBorderColor = puzzleBorderColor
        showPuzzle()
    }

    private fun resetZIndex() {
        for (i in puzzleBlockState!!.indices) {
            for (j in puzzleBlockState!!.indices) {
                ViewCompat.setZ(puzzleBlockState!![i][j]!!.getmImageView(), 0.0f)
            }
        }
    }

    interface OnCompleteListener {
        fun onComplete()
    }

    companion object {
        const val MODE_CRATE = 1
        const val MODE_PLAY = 2
    }
}