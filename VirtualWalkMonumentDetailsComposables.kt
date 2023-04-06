package app.e_skapani.ui.virtual_walk.monument_details.composable

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.foundation.text.selection.SelectionContainer
import androidx.compose.material.Divider
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Alignment.Companion.CenterHorizontally
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.constraintlayout.compose.ConstraintLayout
import app.e_skapani.R
import app.e_skapani.ui.composable.OnBackClicked
import app.e_skapani.ui.composable.bottom_nav_bars.DefaultBottomNavBar
import app.e_skapani.ui.composable.bottom_nav_bars.On3dViewerClicked
import app.e_skapani.ui.composable.bottom_nav_bars.OnArClicked
import app.e_skapani.ui.composable.bottom_nav_bars.OnMapClicked
import app.e_skapani.ui.composable.general.AutoSizeText
import app.e_skapani.ui.composable.general.DefaultScreenSetup
import app.e_skapani.ui.composable.general.EskapaniBody2Text
import app.e_skapani.ui.composable.info_row.InfoRow
import app.e_skapani.ui.composable.noRippleClickable
import app.e_skapani.ui.composable.toolbars.ToolbarBack
import app.e_skapani.ui.theme.*
import app.e_skapani.ui.virtual_walk.VirtualWalkViewModel
import app.e_skapani.ui.virtual_walk.model.VirtualWalkMonumentUiItem
import app.e_skapani.ui.virtual_walk.monument_selector.composable.OnMonumentClicked
import com.google.accompanist.pager.*
import kotlinx.coroutines.launch

typealias OnPhotoGalleryClicked = (listIndex: Int, listOfImages: IntArray) -> Unit

@Composable
fun VirtualWalkMonumentDetailsScreen(viewModel: VirtualWalkViewModel) {

    val selectedMonument by viewModel.selectedMonumentUi.observeAsState()

    VirtualWalkMonumentDetailsContent(
        selectedMonument = selectedMonument!!,
        onBackClicked = viewModel::onBackClicked,
        onMonumentClicked = viewModel::onMonumentClicked,
        onPhotoGalleryClicked = viewModel::onPhotoGalleryClicked,
        onMapClicked = viewModel::onMapClicked,
        onArClicked = viewModel::onArWizardClicked,
        on3dViewerClicked = viewModel::on3dViewerClicked
    )
}

@OptIn(ExperimentalPagerApi::class)
@Composable
fun VirtualWalkMonumentDetailsContent(
    selectedMonument: VirtualWalkMonumentUiItem,
    onBackClicked: OnBackClicked,
    onMonumentClicked: OnMonumentClicked,
    onPhotoGalleryClicked: OnPhotoGalleryClicked,
    onMapClicked: OnMapClicked,
    onArClicked: OnArClicked,
    on3dViewerClicked: On3dViewerClicked
) {
    val pagerState = rememberPagerState()
    DefaultScreenSetup {
        Column {
            ToolbarBack(onBackClicked = onBackClicked)
            Box(Modifier.weight(0.925f)) {
                Column(Modifier.fillMaxSize()) {
                    Box(
                        modifier = Modifier
                            .fillMaxWidth(0.1f)
                            .aspectRatio(1f)
                    )
                    Box(
                        modifier = Modifier
                            .fillMaxSize()
                            .background(Color.White)
                    )
                }

                LazyColumn() {
                    item {
                        Box(
                            modifier = Modifier
                                .fillMaxWidth(0.1f)
                                .aspectRatio(1f)
                        )
                    }
                    item {
                        ConstraintLayout {
                            val (detailsDialog, walkIcon) = createRefs()
                            Column(modifier = Modifier
                                .fillMaxSize()
                                .background(color = Color.White)
                                .constrainAs(detailsDialog) {
                                    top.linkTo(parent.top)
                                    bottom.linkTo(parent.bottom)
                                    start.linkTo(parent.start)
                                    end.linkTo(parent.end)
                                }) {
                                Box(
                                    modifier = Modifier
                                        .fillMaxWidth(0.1f)
                                        .aspectRatio(1f)
                                )
                                VirtualWalkImagesAndText(
                                    selectedMonument = selectedMonument,
                                    onMonumentClicked = onMonumentClicked,
                                    onPhotoGalleryClicked = onPhotoGalleryClicked,
                                    pagerState = pagerState
                                )
                            }
                            Image(
                                modifier = Modifier
                                    .constrainAs(walkIcon) {
                                        top.linkTo(parent.top)
                                        bottom.linkTo(parent.top)
                                        start.linkTo(parent.start)
                                        end.linkTo(parent.end)
                                    }
                                    .fillMaxWidth(0.2f)
                                    .aspectRatio(1f)
                                    .background(Color.White, shape = CircleShape)
                                    .padding(SpacingHalf_8dp),
                                painter = painterResource(id = R.drawable.ic_walk_man),
                                contentDescription = ""
                            )
                        }
                    }
                }
            }
            DefaultBottomNavBar(
                modifier = Modifier.weight(0.075f),
                onMapClicked = onMapClicked,
                onArClicked = onArClicked,
                on3dViewerClicked = on3dViewerClicked,
                isArEnabled = selectedMonument.isArEnabled,
                is3dViewerEnabled = selectedMonument.is3dViewerEnabled
            )
        }
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
fun VirtualWalkImagesAndText(
    selectedMonument: VirtualWalkMonumentUiItem,
    onMonumentClicked: OnMonumentClicked,
    onPhotoGalleryClicked: OnPhotoGalleryClicked,
    pagerState: PagerState
) {
    Column {
        VirtualWalkMonumentSelectionImages(
            selectedMonument = selectedMonument,
            onMonumentClicked = onMonumentClicked,
            pagerState = pagerState
        )
        InfoRow(textRes = selectedMonument.smallDescription)
        Divider(color = Color.LightGray, thickness = 1.dp)
        VirtualWalkMonumentInformation(selectedMonument = selectedMonument)
        Divider(color = Color.LightGray, thickness = 1.dp)
        VirtualWalkMonumentImageCarousel(
            selectedMonument = selectedMonument,
            onPhotoGalleryClicked = onPhotoGalleryClicked,
            pagerState = pagerState
        )
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
fun VirtualWalkMonumentSelectionImages(
    selectedMonument: VirtualWalkMonumentUiItem,
    onMonumentClicked: OnMonumentClicked,
    pagerState : PagerState
) {
    val coroutineScope = rememberCoroutineScope()
    Column {
        Row(
            modifier = Modifier
                .fillMaxWidth()
                .wrapContentHeight(),
            horizontalArrangement = Arrangement.SpaceEvenly
        ) {
            VirtualWalkMonumentUiItem.values().forEach { monument ->
                Image(
                    modifier = Modifier
                        .weight(0.2f)
                        .aspectRatio(2f)
                        .padding(horizontal = SpacingHalf_8dp)
                        .noRippleClickable {
                            onMonumentClicked(monument)
                            coroutineScope.launch {
                                pagerState.animateScrollToPage(0)
                            }
                        },
                    painter = painterResource(id = if (monument == selectedMonument) monument.bubbleIcon else monument.smallIcon),
                    contentDescription = ""
                )
            }
        }
        when (selectedMonument.title) {
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
                        .align(Alignment.End),
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
}

@Composable
fun VirtualWalkMonumentInformation(
    selectedMonument: VirtualWalkMonumentUiItem
) {
    AutoSizeText(
        modifier = Modifier
            .fillMaxWidth()
            .padding(top = SpacingDefault_16dp),
        text = (stringResource(R.string.information_label)),
        textAlign = TextAlign.Center,
        maxFontSize = 18.sp,
        color = ColorGenericBlue
    )
    SelectionContainer {
        AutoSizeText(
            modifier = Modifier
                .fillMaxWidth()
                .padding(SpacingDefault_16dp),
            text = (stringResource(selectedMonument.fullDescription)),
            textAlign = TextAlign.Start,
            fontFamily = digFontFamily,
            color = ColorMonumentSelectorText,
            maxFontSize = 18.sp
        )
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
fun VirtualWalkMonumentImageCarousel(
    selectedMonument: VirtualWalkMonumentUiItem,
    onPhotoGalleryClicked: OnPhotoGalleryClicked,
    pagerState : PagerState
) {

    Column {
        AutoSizeText(
            modifier = Modifier
                .fillMaxWidth()
                .padding(vertical = SpacingHalf_8dp),
            text = (stringResource(R.string.photo_gallery_label)),
            textAlign = TextAlign.Center,
            maxFontSize = 18.sp,
            color = ColorGenericBlue
        )
        HorizontalPager(
            modifier = Modifier,
            count = selectedMonument.listOfImages.size,
            state = pagerState
        ) { page ->
            // Our page content
            Column {
                Image(
                    modifier = Modifier
                        .fillMaxWidth()
                        .aspectRatio(2f)
                        .noRippleClickable {
                            onPhotoGalleryClicked(
                                page,
                                selectedMonument.listOfImages
                            )
                        }
                        .padding(
                            bottom = SpacingQuarter_4dp,
                            start = SpacingDefault_16dp,
                            end = SpacingDefault_16dp
                        ),
                    painter = painterResource(id = selectedMonument.listOfImages[page]),
                    contentDescription = ""
                )
            }
        }
        Column(
            modifier = Modifier
                .fillMaxWidth()
                .wrapContentHeight()
                .align(CenterHorizontally)
        ) {
            HorizontalPagerIndicator(
                pagerState = pagerState,
                modifier = Modifier
                    .align(CenterHorizontally)
                    .padding(SpacingDefault_16dp),
                activeColor = ColorPagerIndicatorSelected,
                inactiveColor = ColorPagerIndicatorUnselected
            )
        }
        if (pagerState.currentPage in selectedMonument.listOfImageDescription.indices) {
            EskapaniBody2Text(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(horizontal = SpacingHalf_8dp)
                    .padding(bottom = SpacingDefault_16dp),
                text = stringResource(id = selectedMonument.listOfImageDescription[pagerState.currentPage]),
                textAlign = TextAlign.Center
            )
        }
    }
}


@Preview(showBackground = true, showSystemUi = true)
@Composable
fun VirtualWalkMonumentDetailsScreenPreview() {
    EskapaniTheme {
        VirtualWalkMonumentDetailsContent(
            selectedMonument = VirtualWalkMonumentUiItem.IPONDROMOS,
            {}, {}, { _, _ -> }, {}, {}, {}
        )
    }
}