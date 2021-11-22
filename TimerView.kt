package app.e_skapani.ui.mosaic.mosaic_puzzle

import android.animation.ValueAnimator
import android.animation.ValueAnimator.AnimatorUpdateListener
import android.annotation.SuppressLint
import android.content.Context
import android.content.res.TypedArray
import android.graphics.*
import android.util.AttributeSet
import android.view.View
import android.view.animation.LinearInterpolator
import app.e_skapani.R
import java.util.concurrent.TimeUnit
import java.util.regex.Pattern


class TimerView(context: Context, attrs: AttributeSet?, defStyleAttr: Int) :
    View(context, attrs, defStyleAttr) {

    private lateinit var mBitmap: Bitmap
    private lateinit var mCanvas: Canvas

    private lateinit var mCircleOuterBounds: RectF
    private lateinit var mCircleInnerBounds: RectF

    private val mCirclePaint: Paint

    private val textPaint: Paint

    private val backgroundPaint: Paint

    private var mCircleSweepAngle = 0f
    private var mTimerAnimator: ValueAnimator? = null

    var textstring: String = "00"

    private var wi: Float = 0.0f
    private var hi: Float = 0.0f

    constructor(context: Context) : this(context, null) {}
    constructor(context: Context, attrs: AttributeSet?) : this(context, attrs, 0) {}

    override fun onMeasure(widthMeasureSpec: Int, heightMeasureSpec: Int) {
        super.onMeasure(widthMeasureSpec, widthMeasureSpec) // Trick to make the view square
    }

    override fun onSizeChanged(w: Int, h: Int, oldw: Int, oldh: Int) {
        super.onSizeChanged(w, h, oldw, oldh)
        wi = w.toFloat()
        hi = h.toFloat()
        mBitmap = Bitmap.createBitmap(w, h, Bitmap.Config.ARGB_8888)
        mCanvas = Canvas(mBitmap)
        updateBounds()
    }

    @SuppressLint("DrawAllocation")
    override fun onDraw(canvas: Canvas) {
        drawOuterBackground(canvas)
        drawTimerLine(canvas)
        drawBackground(canvas)
        drawTimerCountText(canvas)
    }

    private fun drawBackground(canvas: Canvas) {
        val backgroundCircleStartPoint = PointF((width / 2).toFloat(), (height / 2).toFloat())
        val backgroundCircleRadius = ((width - width * THICKNESS_SCALE) / 2).toFloat()
        canvas.drawCircle(
            backgroundCircleStartPoint.x,
            backgroundCircleStartPoint.y,
            backgroundCircleRadius,
            backgroundPaint
        )
    }

    private fun drawOuterBackground(canvas: Canvas) {
        val backgroundCircleStartPoint = PointF((width / 2).toFloat(), (height / 2).toFloat())
        val backgroundCircleRadius = (width / 2).toFloat()
        canvas.drawCircle(
            backgroundCircleStartPoint.x,
            backgroundCircleStartPoint.y,
            backgroundCircleRadius,
            backgroundPaint
        )
    }


    private fun drawTimerCountText(canvas: Canvas) {
        setTextSize()
        val xPos = (width / 2).toFloat()
        val yPos = (height / 2 - (textPaint.descent() + textPaint.ascent()) / 2)
        canvas.drawText(
            textstring,
            xPos,
            yPos,
            textPaint
        )
    }

    private fun setTextSize() {
        when {
            Pattern.compile("[0-9]{2}").matcher(textstring).matches() -> {
                textPaint.textSize = (width / 2).toFloat()
            }
            Pattern.compile("[0-9]{2}:[0-9]{2}").matcher(textstring).matches() -> {
                textPaint.textSize = (width / 3).toFloat()
            }
            Pattern.compile("[0-9]{2}:[0-9]{2}:[0-9]{2}").matcher(textstring).matches() -> {
                textPaint.textSize = (width / 4.5).toFloat()
            }
        }
    }

    private fun drawTimerLine(canvas: Canvas) {
        if (mCircleSweepAngle > 0f) {
            canvas.drawArc(
                mCircleOuterBounds,
                ARC_START_ANGLE.toFloat(),
                mCircleSweepAngle,
                true,
                mCirclePaint
            )
        }
    }

    fun start(secs: Int) {
        stopWithOutLine()
        mTimerAnimator = ValueAnimator.ofFloat(0f, 1f)
        mTimerAnimator?.duration = TimeUnit.SECONDS.toMillis(secs.toLong())
        mTimerAnimator?.interpolator = LinearInterpolator()
        mTimerAnimator?.repeatCount = ValueAnimator.INFINITE
        mTimerAnimator?.addUpdateListener(AnimatorUpdateListener { animation ->
            drawProgress(
                animation.animatedValue as Float
            )
        })
        mTimerAnimator?.start()
    }

    fun stopWithOutLine() {
        if (mTimerAnimator != null && mTimerAnimator!!.isRunning) {
            mTimerAnimator!!.cancel()
            mTimerAnimator = null
            drawProgress(0f)
        }
    }

    fun stopWitLine() {
        if (mTimerAnimator != null && mTimerAnimator!!.isRunning) {
            mTimerAnimator!!.cancel()
            mTimerAnimator = null
        }
    }

    private fun drawProgress(progress: Float) {
        mCircleSweepAngle = 360 * progress
        invalidate()
    }

    private fun updateBounds() {
        val thickness = width * THICKNESS_SCALE
        mCircleOuterBounds = RectF(0f, 0f, width.toFloat(), height.toFloat())
        mCircleInnerBounds = RectF(
            mCircleOuterBounds.left + thickness,
            mCircleOuterBounds.top + thickness,
            mCircleOuterBounds.right - thickness,
            mCircleOuterBounds.bottom - thickness
        )
        invalidate()
    }

    companion object {
        private const val ARC_START_ANGLE = 270 // 12 o'clock
        private const val THICKNESS_SCALE = 0.06f
    }

    init {
        var circleColor: Int = getContext().getColor(R.color.timer_circle_color)
        if (attrs != null) {
            val ta: TypedArray = context.obtainStyledAttributes(attrs, R.styleable.TimerView)
            circleColor = ta.getColor(R.styleable.TimerView_circleColor, circleColor)
            ta.recycle()
        }
        mCirclePaint = Paint()
        mCirclePaint.isAntiAlias = true
        mCirclePaint.color = circleColor

        textPaint = Paint()
        textPaint.style = Paint.Style.FILL
        textPaint.color = getContext().getColor(R.color.timer_text_color)
        textPaint.textAlign = Paint.Align.CENTER

        backgroundPaint = Paint()
        backgroundPaint.color = getContext().getColor(R.color.timer_background_color)
        backgroundPaint.style = Paint.Style.FILL
        backgroundPaint.isAntiAlias = true;
    }
}