package app.e_skapani.ui.virtual_walk.monument_selector.composable

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.animation.core.*
import androidx.compose.animation.fadeIn
import androidx.compose.animation.slideInVertically
import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Alignment.Companion.Center
import androidx.compose.ui.Alignment.Companion.CenterEnd
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.scale
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalConfiguration
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.constraintlayout.compose.ConstraintLayout
import app.e_skapani.R
import app.e_skapani.ui.composable.general.AutoSizeText
import app.e_skapani.ui.composable.general.DefaultScreenSetup
import app.e_skapani.ui.composable.general.EskapaniBody2Text
import app.e_skapani.ui.composable.noRippleClickable
import app.e_skapani.ui.composable.toolbars.OnMenuClicked
import app.e_skapani.ui.composable.toolbars.ToolbarMenu
import app.e_skapani.ui.dashboard.DashboardViewModel
import app.e_skapani.ui.theme.*
import app.e_skapani.ui.virtual_walk.VirtualWalkViewModel
import app.e_skapani.ui.virtual_walk.model.VirtualWalkMonumentUiItem

typealias OnMonumentClicked = (VirtualWalkMonumentUiItem) -> Unit
typealias OnShowMonumentDetailsClicked = () -> Unit


@Composable
fun VirtualWalkMonumentSelectorScreen(
    viewModel: VirtualWalkViewModel,
    dashboardViewModel: DashboardViewModel
) {
    val selectedMonument by viewModel.selectedMonumentUi.observeAsState()

    VirtualWalkMonumentSelectorContent(
        selectedMonument = selectedMonument,
        onMenuClicked = dashboardViewModel::onMenuClicked,
        onMonumentClicked = viewModel::onMonumentClicked,
        onShowMonumentDetailsClicked = viewModel::onShowMonumentDetailsClicked
    )
}

@Composable
fun VirtualWalkMonumentSelectorContent(
    selectedMonument: VirtualWalkMonumentUiItem?,
    onMenuClicked: OnMenuClicked,
    onMonumentClicked: OnMonumentClicked,
    onShowMonumentDetailsClicked: OnShowMonumentDetailsClicked
) {
    val state = remember {
        MutableTransitionState(false).apply {
            targetState = true
        }
    }
    Box(Modifier.fillMaxSize()) {
        DefaultScreenSetup {
            val density = LocalDensity.current
            Column(
                modifier = Modifier.fillMaxSize(),
                verticalArrangement = Arrangement.SpaceBetween,
                horizontalAlignment = Alignment.CenterHorizontally
            ) {
                ToolbarMenu(onMenuClicked = onMenuClicked)
                AnimatedVisibility(visibleState = state,
                    enter = slideInVertically(animationSpec = tween(durationMillis = 800)) {
                        with(density) { 60.dp.roundToPx() }
                    } + fadeIn(
                        initialAlpha = 0.3f,
                        animationSpec = tween(durationMillis = 2000)
                    )) {
                    VirtualWalkBottomSelector(
                        selectedMonument = selectedMonument,
                        onMonumentClicked = onMonumentClicked,
                        onShowMonumentDetailsClicked = onShowMonumentDetailsClicked
                    )
                }
            }
        }
        AnimatedVisibility(
            visibleState = state,
            enter = fadeIn(animationSpec = tween(durationMillis = 600))
        ) {
            VirtualWalkBubbleMap(
                onMonumentClicked = onMonumentClicked
            )
        }
    }
}

@Composable
fun VirtualWalkBubbleMap(
    onMonumentClicked: OnMonumentClicked
) {
    val screenWidthDp = LocalConfiguration.current.screenWidthDp
    val screenHeightDp = LocalConfiguration.current.screenHeightDp

    Box(modifier = Modifier.fillMaxSize()) {
        VirtualWalkMonumentUiItem.values().forEach { virtualWalkMonument ->
            Image(
                modifier = Modifier
                    .padding(
                        start = screenWidthDp.times(virtualWalkMonument.bubblePositionWidth).dp,
                        top = screenHeightDp.times(virtualWalkMonument.bubblePositionHeight).dp
                    )
                    .width(screenWidthDp.div(8).dp)
                    .height(screenHeightDp.div(8).dp)
                    .noRippleClickable { onMonumentClicked(virtualWalkMonument) },
                painter = painterResource(virtualWalkMonument.bubbleIcon),
                contentDescription = ""
            )
        }
    }
}

@Composable
fun VirtualWalkBottomSelector(
    selectedMonument: VirtualWalkMonumentUiItem?,
    onMonumentClicked: OnMonumentClicked,
    onShowMonumentDetailsClicked: OnShowMonumentDetailsClicked
) {
    Box(Modifier.padding(bottom = SpacingDefault_16dp)) {
        ConstraintLayout {
            val (selector, walkIcon, arrowIcon) = createRefs()
            Column(
                modifier = Modifier
                    .fillMaxWidth(0.9f)
                    .aspectRatio(1.8f)
                    .background(Color.White, shape = RoundedCornerShape(size = SpacingHalf_8dp))
                    .constrainAs(selector) {
                        start.linkTo(parent.start)
                        end.linkTo(parent.end)
                        top.linkTo(parent.top)
                        bottom.linkTo(parent.bottom)
                    }
            ) {
                //Paddings from top for walk icon
                Box(modifier = Modifier
                    .fillMaxWidth(1 / 9f)
                    .aspectRatio(1f))
                VirtualWalkImagesAndTexts(
                    selectedMonument = selectedMonument,
                    onMonumentClicked = onMonumentClicked
                )
                //Paddings from bottom for arrow icon
                Box(modifier = Modifier
                    .fillMaxWidth((0.75 / 9).toFloat())
                    .aspectRatio(1f))
            }

            Box(
                modifier = Modifier
                    .padding(start = SpacingCustom_20dp)
                    .fillMaxWidth(0.2f)
                    .aspectRatio(1f)
                    .background(Color.White, shape = CircleShape)
                    .constrainAs(walkIcon) {
                        start.linkTo(parent.start)
                        top.linkTo(selector.top)
                        bottom.linkTo(selector.top)
                    }
            ) {
                Image(
                    modifier = Modifier
                        .fillMaxSize()
                        .padding(SpacingHalf_8dp),
                    painter = painterResource(id = R.drawable.ic_walk_man),
                    contentDescription = ""
                )
            }

            if (selectedMonument != null) {
                val scale = rememberInfiniteTransition().animateFloat(
                    initialValue = 0.75f,
                    targetValue = 1f,
                    animationSpec = infiniteRepeatable(
                        tween(800),
                        RepeatMode.Reverse
                    )
                )

                Box(
                    modifier = Modifier
                        .padding(end = SpacingDouble_32dp)
                        .fillMaxWidth(0.15f)
                        .aspectRatio(1f)
                        .background(
                            color = secondaryColor,
                            shape = CircleShape
                        )
                        .noRippleClickable { onShowMonumentDetailsClicked() }
                        .constrainAs(arrowIcon) {
                            end.linkTo(parent.end)
                            top.linkTo(selector.bottom)
                            bottom.linkTo(selector.bottom)
                        }
                        .scale(scale.value)
                ) {
                    Image(
                        modifier = Modifier.fillMaxSize(),
                        painter = painterResource(id = R.drawable.ic_double_arrow),
                        contentDescription = ""
                    )
                }
            }
        }
    }
}

@Composable
fun ColumnScope.VirtualWalkImagesAndTexts(
    selectedMonument: VirtualWalkMonumentUiItem?,
    onMonumentClicked: OnMonumentClicked
) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .weight(0.3f)
            .wrapContentHeight(),
        horizontalArrangement = Arrangement.SpaceEvenly
    ) {
        VirtualWalkMonumentUiItem.values().forEach { monument ->
            Image(
                modifier = Modifier
                    .weight(0.2f)
                    .aspectRatio(2f)
                    .padding(horizontal = SpacingHalf_8dp)
                    .noRippleClickable { onMonumentClicked(monument) },
                painter = painterResource(id = if (monument == selectedMonument) monument.bubbleIcon else monument.smallIcon),
                contentDescription = ""
            )
        }
    }
    Box(modifier = Modifier
        .fillMaxWidth()
        .padding(vertical = SpacingQuarter_4dp)) {
        when (selectedMonument?.title) {
            R.string.rotunda_title_label -> {
                EskapaniBody2Text(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(start = SpacingHalf_8dp),
                    text = stringResource(id = R.string.rotunda_title_label),
                    textAlign = TextAlign.Start
                )
            }
            R.string.arch_of_galerius_title_label -> {
                EskapaniBody2Text(
                    modifier = Modifier.fillMaxWidth(0.6f),
                    text = stringResource(id = R.string.arch_of_galerius_title_label),
                    textAlign = TextAlign.Center
                )
            }
            R.string.hippodrome_title_label -> {
                EskapaniBody2Text(
                    modifier = Modifier.fillMaxWidth(),
                    text = stringResource(id = R.string.hippodrome_title_label),
                    textAlign = TextAlign.Center
                )
            }
            R.string.octagon_title_label -> {
                EskapaniBody2Text(
                    modifier = Modifier
                        .fillMaxWidth(0.6f)
                        .align(CenterEnd),
                    text = stringResource(id = R.string.octagon_title_label),
                    textAlign = TextAlign.Center
                )
            }
            R.string.vasiliki_title_label -> {
                EskapaniBody2Text(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(end = SpacingHalf_8dp),
                    text = stringResource(id = R.string.vasiliki_title_label),
                    textAlign = TextAlign.End
                )
            }
            else -> {
                EskapaniBody2Text(
                    modifier = Modifier.fillMaxWidth(),
                    text = stringResource(id = R.string.empty)
                )
            }
        }
    }
    VirtualWalkBottomSelectorText(
        modifier = Modifier
            .fillMaxWidth()
            .weight(0.5f),
        selectedMonument = selectedMonument
    )
}

@Composable
fun VirtualWalkBottomSelectorText(
    modifier: Modifier,
    selectedMonument: VirtualWalkMonumentUiItem?
) {
    if (selectedMonument != null) {
        Row(
            modifier = Modifier
                .fillMaxWidth()
                .wrapContentHeight()
                .background(ColorInfoBackground),
            verticalAlignment = Alignment.CenterVertically
        ) {
            Image(
                modifier = Modifier.weight(0.15f),
                painter = painterResource(id = R.drawable.ic_info_black),
                contentDescription = stringResource(id = R.string.empty)
            )
            AutoSizeText(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(vertical = SpacingCustom_6dp)
                    .padding(end = SpacingHalf_8dp)
                    .weight(0.85f),
                text = (stringResource(selectedMonument.smallDescription)),
                fontWeight = FontWeight.Bold,
                textAlign = TextAlign.Center,
                color = Color.White,
                maxLines = 2,
                maxFontSize = 18.sp
            )
        }
    } else {
        Box(
            modifier = modifier,
            contentAlignment = Center,
        ) {
            AutoSizeText(
                modifier = Modifier.padding(SpacingCustom_12dp),
                text = (stringResource(R.string.digital_walk_chooser_info_label)),
                textAlign = TextAlign.Center,
                maxFontSize = 18.sp
            )
        }
    }
}

@Preview(showBackground = true, showSystemUi = true)
@Composable
fun VirtualWalkMonumentSelectorScreenPreview() {
    EskapaniTheme {
        VirtualWalkMonumentSelectorContent(
            selectedMonument = VirtualWalkMonumentUiItem.IPONDROMOS,
            {}, {}, {})
    }
}